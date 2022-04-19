using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] Button home;
    [SerializeField] List<Button> levels;
    [SerializeField] List<GameObject> levelLockers;
    [SerializeField] List<Image> medals;
    [SerializeField] List<Sprite> medalType;

    // Start is called before the first frame update
    void Start()
    {
        int[] levelMedal = GameManager.instance.GetLevelMedal();

        ////TODO: LEVELSCORE TEST REMOVE LATER
        //GameManager.instance.levelScore[0] = 200;

        //GameManager.instance.levelScore[1] = 100;

        //GameManager.instance.levelScore[2] = 130;

        //GameManager.instance.levelScore[3] = 70;
        ////TODO: LEVELSCORE TEST REMOVE END

        for(int i = 0; i < levels.Count; i++)
        {
            if (i >= levelMedal.Length)
            {
                medals[i].sprite = null;
                medals[i].color = Color.clear;
            }
            else if (levelMedal[i] == 0)
            {
                medals[i].sprite = null;
                medals[i].color = Color.clear;
            }
            else
            {
                medals[i].sprite = medalType[levelMedal[i] - 1];
                medals[i].color = Color.white;
            }
        }

        home.onClick.AddListener(onHome);

        UpdateLevelEnable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevel(int i)
    {
        SceneManager.LoadScene("Level " + i);
        GameManager.instance.currentLevel = i;
    }

    void onHome()
    {
        SceneManager.LoadScene("StartScene");
    }

    void UpdateLevelEnable()
    {
        int[] levelEnable = GameManager.instance.GetLevelEnable();

        for (int i = 0; i < levelEnable.Length; i++)
        {
            if (levelEnable[i] == 1)
            {
                levels[i].enabled = true;
                levelLockers[i].SetActive(false);
            }
            else
            {
                levels[i].enabled = false;
                levelLockers[i].SetActive(true);
            }
        }
    }
}
