using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
        else
        {
            Destroy(gameObject);
        }

    }
    public void PlaySFX(int _sfxIndex)
    {
        if (_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].Play();
        }
    }
    public void PlayBGM(int _bgmIndex)
    {
        if(_bgmIndex < bgm.Length)
        {
            bgm[_bgmIndex].Play();
        }
    }
    public void StopBGM(int _bgmIndex)
    {
        if(_bgmIndex < bgm.Length)
        {
            bgm[_bgmIndex].Stop();
        }
    }
    public void StopSFX(int _sfxIndex)
    {
        if(_sfxIndex < sfx.Length)
        {
            sfx[_sfxIndex].Stop();
        }
    }
}
