using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public static MySceneManager Instance;
    static public int totalScore = 0;
    static public int levelScore = 0;
    static public int blood = 3;
    static public int level = 1;
    static public bool isGameOver = false;
    public int CurrentSceneIndex { get; private set; }
    public int EnemyCount {  get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        
    }

    public static void AddScore(int add)
    {
        totalScore += add;
        levelScore += add;

    }
    public static void AddBlood(int add)
    {
        blood = Mathf.Min(blood + add, 3);
        if (blood == 0)
        {
            UiManager.Instance.ShowLevelFailed();
            isGameOver = true;
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CurrentSceneIndex = scene.buildIndex;
        blood= 3;
        levelScore = 0;
        isGameOver = false;

        UpdateEnemyCount();
    }
    public void UpdateEnemyCount()
    {
        EnemyCount = FindObjectsOfType<Enemy>().Length;
    }
    public void OnEnemyDefeated()
    {
        AddScore(100);
        EnemyCount--;
        if (EnemyCount == 0)
        {
            UiManager.Instance.ShowLevelComplete();
           isGameOver = true;
        }
    }
}
