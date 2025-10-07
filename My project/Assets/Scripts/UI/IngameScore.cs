using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class IngameScore : MonoBehaviour
{
    private TextMeshProUGUI valueText;
    private int lastScore = 0;

    void Start()
    {
       
        valueText = GetComponent<TextMeshProUGUI>();

        
        UpdateScoreDisplay();
    }

    void Update()
    {
        
        if (MySceneManager.levelScore != lastScore)
        {
            UpdateScoreDisplay();
            lastScore = MySceneManager.levelScore;
        }
    }

    void UpdateScoreDisplay()
    {
        
            valueText.text = MySceneManager.levelScore.ToString();
       
    }
}
