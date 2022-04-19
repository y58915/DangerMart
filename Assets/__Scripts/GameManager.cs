using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int levelCount;

    public int currentLevel = 0;

    int soundOn = 1;         //0: off; 1: on
    int[] levelScore;
    int[] levelMedal;    //0: none; 1: bronze; 2: silver; 3: gold

    [SerializeField] int[] levelMaxScore;
    [SerializeField] int[] levelEnable;

    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            levelScore = new int[levelCount];
            levelMedal = new int[levelCount];
            levelEnable = new int[levelCount];

            ReadScore();
            ReadSoundSetting();
            ReadLevelEnables();
            DontDestroyOnLoad(this);
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

    void ReadSoundSetting()
    {
        soundOn = PlayerPrefs.GetInt("Sound", 1);
    }

    void ReadLevelEnables()
    {
        levelEnable[0] = PlayerPrefs.GetInt("LevelEnable 1", 1);
        for (int i = 1; i < levelCount; i++)
        {
            levelEnable[i] = PlayerPrefs.GetInt("LevelEnable " + (i + 1), 0);
        }
    }

    //level start from 1
    public void SetScore(int level, int score, int medal)
    {
        if (medal != 0 && level != 3)
        {
            SetLevelEnable(level + 1);
        }

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

    public void SetLevelEnable(int level)
    {
        levelEnable[level] = 1;

        SaveScore();
    }

    public void SaveLevelEnables()
    {
        for (int i = 0; i < levelCount; i++)
        {
            PlayerPrefs.SetInt("LevelEnable " + (i + 1), levelEnable[i]);
        }

        SavePrefs();
    }

    public void SetSound(bool tf)
    {
        soundOn = tf ? 1 : 0;
    }

    public bool GetSound()
    {
        return soundOn == 1;
    }

    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }

    public int[] GetLevelMedal()
    {
        return levelMedal;
    }

    public int[] GetLevelEnable()
    {
        return levelEnable;
    }

    public int GetMaxScore()
    {
        if (currentLevel > levelMaxScore.Length) return 0;
        return levelMaxScore[currentLevel - 1];
    }


    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("Sound", soundOn);
        SavePrefs();
    }
}
