using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance;
    public float minSpawnInterval;
    public float maxSpawnInterval;
    public float buffExistDuration;

    private float spawnTimer;
    private float currentSpawnInterval;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<GameObject> buffPrefabs = new List<GameObject>();
    [SerializeField] private List<GameObject> dropPrefabs = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            LoadItemsPrefabs();
        }
        else
        {
            Destroy(gameObject);
        }
        LoadItemsPrefabs();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindSpawnPoints();
        currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        spawnTimer = 0f;
    }

    void FindSpawnPoints()
    {
        spawnPoints.Clear();
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            foreach (Transform point in spawnPoint.transform)
            {
                spawnPoints.Add(point);
            }
        }
    }

    void LoadItemsPrefabs()
    {
        
        dropPrefabs.Clear();
        buffPrefabs.Clear();
        GameObject[] foundDrops = Resources.LoadAll<GameObject>("Drops");
        foreach (GameObject drop in foundDrops)
        {
            dropPrefabs.Add(drop);
        }
        GameObject[] foundBuffs = Resources.LoadAll<GameObject>("Buffs");
        foreach (GameObject buff in foundBuffs)
        {
            buffPrefabs.Add(buff);
        }
    }
    
    

    void Update()
    {
        if (MySceneManager.isGameOver) return;

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnInterval)
        {
            SpawnBuff();
            spawnTimer = 0f;
            currentSpawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }

    void SpawnBuff()
    {
        int pointIndex = Random.Range(0, spawnPoints.Count);
        int buffIndex = Random.Range(0, buffPrefabs.Count);

        Vector3 spawnPosition = spawnPoints[pointIndex].position;
        GameObject newBuff = Instantiate(buffPrefabs[buffIndex], spawnPosition, Quaternion.identity);
        Destroy(newBuff, buffExistDuration);

    }
    public void SpawnDrop(Vector3 spawnPosition)
    {

        if (buffPrefabs.Count == 0) return;

        float randomValue = Random.Range(0f, 1f);
        GameObject dropPrefab = null;

        if (randomValue < 0.1f)
        {
            dropPrefab = dropPrefabs.Find(drop => drop.CompareTag("Diamond"));
        }
        else if (randomValue < 0.55f)
        {
            dropPrefab = dropPrefabs.Find(drop => drop.CompareTag("Silver"));
        }
        else
        {
            dropPrefab = dropPrefabs.Find(drop => drop.CompareTag("Gold"));
        }
        Instantiate(dropPrefab, spawnPosition, Quaternion.identity);
    }
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
}