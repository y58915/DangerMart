using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOvereUI : MonoBehaviour
{
    [SerializeField] Image medal;
    [SerializeField] Button restartButton;
    [SerializeField] Button levelButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button nextLevelButton;

    [SerializeField] List<Sprite> medalList;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(OnRestart);
        levelButton.onClick.AddListener(OnLevel);
        quitButton.onClick.AddListener(OnQuit);
        nextLevelButton.onClick.AddListener(OnNextLevel);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMedal(int i)
    {
        if (i == 0)
        {
            medal.enabled = false;
            return;
        }

        medal.enabled = true;
        medal.sprite = medalList[i-1];
    }

    void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void OnNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Level 4")
            SceneManager.LoadScene("StartScene");
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    void OnLevel()
    {
        SceneManager.LoadScene("LevelScene");
    }

    void OnQuit()
    {
        SceneManager.LoadScene("StartScene");
    }
}
