using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager Instance;

    [SerializeField] protected GameObject inGameUI;
    [SerializeField] protected GameObject pauseUI;
    [SerializeField] protected GameObject levelCompleteUI;
    [SerializeField] protected GameObject failedUI;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindUIComponents();

        if (scene.buildIndex == 0)
        {
            SetUIState(false, false, false, false);
            AudioManager.Instance.PlayBGM(0);
        }
        else
        {
            SetUIState(true, false, false, false);
            AudioManager.Instance.StopBGM(0);
            AudioManager.Instance.PlayBGM(1);
            Time.timeScale = 1;
        }
    }

    void FindUIComponents()
    {
        Canvas[] allCanvases = FindObjectsOfType<Canvas>(true);
        foreach (Canvas canvas in allCanvases)
        {
            if (canvas.name == "CanvasInGame") inGameUI = canvas.gameObject;
            if (canvas.name == "CanvasPause") pauseUI = canvas.gameObject;
            if (canvas.name == "CanvasPass") levelCompleteUI = canvas.gameObject;
            if (canvas.name == "CanvasFailed") failedUI = canvas.gameObject; 
        }
    }

    void SetUIState(bool inGame, bool pause, bool levelComplete, bool failed)
    {
        if (inGameUI != null) inGameUI.SetActive(inGame);
        if (pauseUI != null) pauseUI.SetActive(pause);
        if (levelCompleteUI != null) levelCompleteUI.SetActive(levelComplete);
        if (failedUI != null) failedUI.SetActive(failed);
    }

    public void ShowLevelFailed()
    {
        SetUIState(true, false, false, true); 
    }
    void Update()
    {
        if (MySceneManager.isGameOver) { return; }
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex > 0)
        {
            Pause();
        }
    }
    public void Pause()
    {


        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            SetUIState(true, true, false,false);
        }
        else
        {
            Time.timeScale = 1;
            SetUIState(true, false, false,false);
        }
    }


    public void ShowLevelComplete()
    {
        SetUIState(true, false, true,false);
        AudioManager.Instance.PlaySFX(2);
        AudioManager.Instance.StopBGM(1);
    }




    void OnDestroy()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}