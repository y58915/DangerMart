using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuffUI : MonoBehaviour
{
    [SerializeField] Text DebuffText;

    #region Singleton
    public static DebuffUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDebuff(string s)
    {
        StopAllCoroutines();
        DebuffText.gameObject.SetActive(true);
        DebuffText.text = s;
        DebuffText.color = Color.white;
    }


    public void SetDebuff(string s, float time)
    {
        StopAllCoroutines();

        DebuffText.gameObject.SetActive(true);
        DebuffText.text = s;
        DebuffText.color = Color.white;

        StartCoroutine(CleanDebuffAfterSec(time));
    }

    public void CleanDebuff()
    {
        StopAllCoroutines();

        if (!DebuffText.gameObject.activeInHierarchy) return;

        DebuffText.text = "";
        DebuffText.gameObject.SetActive(false);
    }

    IEnumerator CleanDebuffAfterSec(float time)
    {
        yield return new WaitForSeconds(time);

        CleanDebuff();
    }
}
