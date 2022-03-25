using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button homeButton;
    [SerializeField] Button levelButton;
    [SerializeField] Button optionButton;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(OnResume);
        restartButton.onClick.AddListener(OnRestart);
        homeButton.onClick.AddListener(OnHome);
        levelButton.onClick.AddListener(OnLevel);
        optionButton.onClick.AddListener(OnOption);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnResume()
    {
        UIManager.instance.TogglePausePanel();
    }

    void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnHome()
    {
        SceneManager.LoadScene("StartScene");
    }

    void OnLevel()
    {
        SceneManager.LoadScene("LevelScene");
    }

    void OnOption()
    {

    }

}
