using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int levelCount;

    public int currentLevel = 0;

    int[] levelScore;
    int[] levelMedal;    //0: none; 1: bronze; 2: silver; 3: gold

    [SerializeField] int[] levelMaxScore;

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
        levelMedal = new int[levelCount];

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
            levelScore[i] = PlayerPrefs.GetInt("LevelScore " + (i + 1), 0);
        }
        for (int i = 0; i < levelCount; i++)
        {
            levelScore[i] = PlayerPrefs.GetInt("LevelMedal " + (i + 1), 0);
        }
    }

    //level start from 1
    public void SetScore(int level, int score, int medal)
    {
        if (score >= levelScore[level])
        {
            levelScore[level] = score;
            levelMedal[level] = medal;

            SaveScore();
        }

    }

    public void SaveScore()
    {
        for (int i = 0; i < levelCount; i++)
        {
            PlayerPrefs.SetInt("LevelScore " + (i + 1), levelScore[i]);
        }

        for (int i = 0; i < levelCount; i++)
        {
            PlayerPrefs.SetInt("LevelMedal " + (i + 1), levelMedal[i]);
        }

        SavePrefs();
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }

    public int[] GetLevelMedal()
    {
        return levelMedal;
    }

    public int GetMaxScore()
    {
        if (currentLevel > levelMaxScore.Length) return 0;
        return levelMaxScore[currentLevel - 1];
    }
}
