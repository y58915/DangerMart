using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public int levelCount;

    [SerializeField] public int[] levelScore;

    #region Singleton
    public static GameManager instance;

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
        levelScore = new int[levelCount];
        ReadScore();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadScore()
    {
        for (int i = 0; i < levelCount; i++)
        {
            levelScore[i] = PlayerPrefs.GetInt("Level " + (i + 1), 0);
        }
    }

    //level start from 1
    public void SetScore(int level, int score)
    {
        levelScore[level - 1] = score;

        SaveScore();
    }

    public void SaveScore()
    {
        for (int i = 0; i < levelCount; i++)
        {
            PlayerPrefs.SetInt("Level " + (i + 1), levelScore[i]);
        }

        SavePrefs();
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }
}
