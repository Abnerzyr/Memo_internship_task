using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//need EventSystem
public class ButtonConroller : MonoBehaviour
{
    public void LoadFirstScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        
    }
    public void LoadNextScene()
    {
       
        int nextSceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex + 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    public void CoutinueGame()
    {
        UiManager.Instance.Pause();
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

