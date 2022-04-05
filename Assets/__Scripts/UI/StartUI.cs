using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button levelButton;
    [SerializeField] Button optionButton;
    [SerializeField] Button creditButton;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnStart);
        levelButton.onClick.AddListener(OnLevel);
        optionButton.onClick.AddListener(OnOption);
        creditButton.onClick.AddListener(OnCredit);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnStart()
    {
        //SceneManager.LoadScene("Level 1");
        SceneManager.LoadScene("LevelScene");
    }

    void OnOption()
    {

    }

    void OnLevel()
    {
        SceneManager.LoadScene("LevelScene");
    }

    void OnCredit()
    {

    }
}
