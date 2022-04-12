using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button optionButton;

    [Header("Credit")]
    [SerializeField] Button creditButton;
    [SerializeField] Button creditReturnButton;
    [SerializeField] GameObject creditPanel;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(OnStart);
        optionButton.onClick.AddListener(OnOption);
        creditButton.onClick.AddListener(OnCredit);
        creditReturnButton.onClick.AddListener(OnCreditReturn);

        creditPanel.SetActive(false);
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


    void OnCredit()
    {
        creditPanel.SetActive(true);
    }

    void OnCreditReturn()
    {
        creditPanel.SetActive(false);
    }
}
