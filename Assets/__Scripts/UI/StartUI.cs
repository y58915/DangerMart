using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button quitButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnStart);
        optionButton.onClick.AddListener(OnOption);
        quitButton.onClick.AddListener(OnQuit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnStart()
    {
        SceneManager.LoadScene("Level 1");
    }

    void OnOption()
    {

    }

    void OnQuit()
    {
        Application.Quit();
    }
}
