using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConroller : MonoBehaviour
{
    private GameObject[] bloodIndicators;
    private int lastBlood = 0;

    void Start()
    {
       
        bloodIndicators = new GameObject[4];
        for (int i = 0; i < 4; i++)
        {
            bloodIndicators[i] = transform.GetChild(i).gameObject;
        }

        UpdateBloodDisplay();
    }

    void Update()
    {
        if(MySceneManager.blood != lastBlood)
        {
            UpdateBloodDisplay();
            lastBlood = MySceneManager.blood;
        }
        
    }

    
    public void UpdateBloodDisplay()
    {
       
        int currentBlood = MySceneManager.blood;


        for (int i = 0; i < 4; i++)
        {
            bloodIndicators[i].SetActive(i == currentBlood);
           
        }

    }
}

