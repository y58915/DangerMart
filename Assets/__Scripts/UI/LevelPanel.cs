using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] Button home;
    [SerializeField] List<Button> levels;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            levels[i].onClick.AddListener(delegate { GoToLevel(i + 1); });
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
