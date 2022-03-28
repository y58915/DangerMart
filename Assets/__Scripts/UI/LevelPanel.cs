using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] Button home;
    [SerializeField] List<Button> levels;
    [SerializeField] List<GameObject> medals;
    [SerializeField] List<int> scores;

    // Start is called before the first frame update
    void Start()
    {

        //TODO: LEVELSCORE TEST REMOVE LATER
        GameManager.instance.levelScore[0] = 200;

        GameManager.instance.levelScore[1] = 100;

        GameManager.instance.levelScore[2] = 130;

        GameManager.instance.levelScore[3] = 70;
        //TODO: LEVELSCORE TEST REMOVE END

        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].onClick.AddListener(delegate { GoToLevel(i + 1); });
        }

        for(int i = 0; i < GameManager.instance.levelCount; i++)
        {
            if(GameManager.instance.levelScore[i] / (float)scores[i] > .33f)
            {
                medals[i].transform.GetChild(0).gameObject.SetActive(true);
            }
            if (GameManager.instance.levelScore[i] / (float)scores[i]  > .66f)
            {
                medals[i].transform.GetChild(1).gameObject.SetActive(true);
            }
            if (GameManager.instance.levelScore[i] / (float)scores[i] >= 1)
            {
                medals[i].transform.GetChild(2).gameObject.SetActive(true);
            }
        }

        home.onClick.AddListener(onHome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToLevel(int i)
    {
        SceneManager.LoadScene("Level " + i);
    }

    void onHome()
    {
        SceneManager.LoadScene("StartScene");
    }
}
