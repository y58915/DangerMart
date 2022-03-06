using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        resumeButton.onClick.AddListener(OnResume);
        restartButton.onClick.AddListener(OnRestart);
        quitButton.onClick.AddListener(OnQuit);
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

    void OnQuit()
    {
        SceneManager.LoadScene("StartScene");
    }
}
