using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.Analytics;

public class CharacterControl : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent playerNav;
    public Animator playerAnim;
    public GameObject targetDestination;
    public LayerMask groundLayerMask;
    public LayerMask collectionLayerMask;
    public float distance = 0f;
    public int mouse_clicks = 0;

    [SerializeField]
    private Item wildCardRef;
    [SerializeField]
    public PowerupSpawner powerupSpawner; 

    private SoundManager soundManager;
    
    private CollectionArea targetCollectionArea;
    private bool inputEnabled = true;
    private float originalSpeed;
    private Vector3 last = Vector3.zero;
    private InputAction leftMouseClick;
    private bool invulernable = false;


    public float invulernablityTime = 5f;
    public static CharacterControl instance;

    private void Awake()
    {
        leftMouseClick = new InputAction(binding: "<Mouse>/leftButton");
        leftMouseClick.performed += ctx => LeftMouseClicked();
        leftMouseClick.Enable();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void LeftMouseClicked()
    {
        mouse_clicks += 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = playerNav.speed;
        soundManager = GameObject.Find("SoundManager/SFX").GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (last != Vector3.zero)
        {
            distance += Vector3.Distance(last, gameObject.transform.position);
        }
        UpdateAnimations();
        last = gameObject.transform.position;

        if (LevelController.instance.gamePaused)
        {
            playerNav.isStopped = true;
        }
        else
        {
            playerNav.isStopped = false;
        }
    }

    public void UpdateAnimations()
    {
        // Debug.Log("sqrtMagnitude => " + playerNav.velocity.sqrMagnitude);
        //Change animator variables
        if (playerNav.velocity.sqrMagnitude < 0.1f)
        {
            playerAnim.SetBool("isWalking", false);
        }
        else
        {
            playerAnim.SetBool("isWalking", true);
        }
    }

    public void OnMove()
    {


        //Debug.Log("Move to: " + Mouse.current.position.ReadValue());
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hitPoint;

        if (Physics.Raycast(ray, out hitPoint, 1000f, collectionLayerMask) && inputEnabled)
        {
            // Set clicked on collection area as destination
            targetCollectionArea = hitPoint.collider.gameObject.GetComponent<CollectionArea>();

            // Move in fron of collection area to collect item
            targetDestination.transform.position = new Vector3(targetCollectionArea.collectionPosition.position.x, 
                targetCollectionArea.collectionPosition.position.y - 1.0f, 
                targetCollectionArea.collectionPosition.position.z);
            playerNav.SetDestination(targetCollectionArea.collectionPosition.position);

            // Activate collection trigger
            targetCollectionArea.collectionTrigger.enabled = true;
        }
        else if (Physics.Raycast(ray, out hitPoint, 1000f, groundLayerMask) && inputEnabled)
        {
            // Move to point on floor 
            targetDestination.transform.position = hitPoint.point;
            playerNav.SetDestination(hitPoint.point);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        
        Debug.Log("OnCollisionEnter");
        
        if (invulernable)
            return;
        
        if (collision.gameObject.GetComponent<PatrollingEnemy>() != null)
        {
            switch(collision.gameObject.GetComponent<PatrollingEnemy>().enemyType)
            {
                case PatrollingEnemy.EnemyType.Stun:
                    Stun();
                    soundManager.Play("Stun");
                    break;
                case PatrollingEnemy.EnemyType.SlowDown:
                    SlowDown();
                    soundManager.Play("Slow");
                    break;
                case PatrollingEnemy.EnemyType.Steal:
                    LoseItem();
                    soundManager.Play("Steal");
                    break;
                default:
                    break;
            }
            AnalyticsResult analyticsResult_Enemies = Analytics.CustomEvent("Enemy Hit", new Dictionary<string, object> 
            {
                { "Level", LevelController.instance.GetLevel() },
                { "Enemy", collision.gameObject.GetComponent<PatrollingEnemy>().enemyType } 
            });
            // Debug.Log("Enemy Hit: " + analyticsResult_Enemies);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PowerUp>() != null)
        {
            switch(other.gameObject.GetComponent<PowerUp>().powerupItem.type)
            {
                case PowerupItem.PowerUpAbility.SpeedBoost:
                    if (invulernable)
                        return;
                    SpeedUp();
                    break;
                case PowerupItem.PowerUpAbility.WildCard:
                    AddWildCard();
                    break;
                case PowerupItem.PowerUpAbility.EnergyDrink:
                    IncreaseEnergy();
                    break;
                default:
                    break;
            }
            Destroy(other.gameObject);
            powerupSpawner.DecrementNumberOfPowerUps();
        }
    }

    private void Stun()
    {
        Debug.Log("Stunning Effect Triggered");
        DebuffUI.instance.SetDebuff("STUNNED");

        playerNav.isStopped = true;
        playerNav.SetDestination(this.transform.position);
        playerAnim.enabled = false;
        inputEnabled = false;

        StartCoroutine(StunTime(5.0f));
        playerAnim.enabled = true;

    }

    private IEnumerator StunTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerNav.isStopped = false;
        inputEnabled = true;

        DebuffUI.instance.CleanDebuff();
    }

    private void SlowDown()
    {
        DebuffUI.instance.SetDebuff("SLOW");

        playerNav.speed = originalSpeed * 0.25f;

        StartCoroutine(SpeedChange(5.0f));

    }

    private void SpeedUp()
    {
        DebuffUI.instance.SetDebuff("SPEED UP!");

        playerNav.speed = originalSpeed * 2.0f;

        StartCoroutine(SpeedChange(5.0f));
    }

    private IEnumerator SpeedChange(float duration)
    {
        yield return new WaitForSeconds(duration);
        playerNav.speed = originalSpeed;

        DebuffUI.instance.CleanDebuff();
    }

    private void LoseItem()
    {
        List<Item> items = new List<Item>(Inventory.instance.container);
        if (items.Count == 0)
        {
            return;
        }
        int index = Random.Range(0, items.Count);

        Inventory.instance.StealItem(items[index]);

        DebuffUI.instance.SetDebuff("ITEM STOLEN", 1f);
    }

    private void AddWildCard()
    {
        Inventory.instance.addItemEvent.Invoke(wildCardRef);

        DebuffUI.instance.SetDebuff("GOT A WILDCARD!", 1f);
    }

    private void IncreaseEnergy()
    {
        EnergyBar.instance.AddEnergy();

        DebuffUI.instance.SetDebuff("ENERGY INCREASED!", 1f);
    }

    public void UseEnergy()
    {
        DebuffUI.instance.SetDebuff("INVULNERABLE!");
        invulernable = true;
        playerNav.speed = originalSpeed * 2.0f;
        // originalSpeed = originalSpeed * 2.0f; //update original speed as well? we can discuss game design later
        StartCoroutine("RemoveEnergyBuff");
    }

    IEnumerator RemoveEnergyBuff()
    {
        yield return new WaitForSeconds(invulernablityTime);
        invulernable = false;
        playerNav.speed = originalSpeed;
        DebuffUI.instance.CleanDebuff();
    }
    
}