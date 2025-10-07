using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtCheckConroller : MonoBehaviour
{
    public bool isHurt=false;
    public bool isinvincible = false;
    private float invincibleTimer;
    public float invincibleTime = 3f;
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")&&isHurt==false&&isinvincible==false)
        {
            isHurt = true;
            if (MySceneManager.blood>0)
            {
                isinvincible = true;  
            }
            
        
        }
        
    }
    private void Start()
    {
        invincibleTimer = invincibleTime;
    }
    private void Update()
    {
        if (isinvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                isinvincible = false;
                invincibleTimer = 3f;
            }
        }
    }
}

