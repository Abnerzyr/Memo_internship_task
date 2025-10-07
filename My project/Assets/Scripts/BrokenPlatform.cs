using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    public Animator anim;
    [SerializeField] private Collider2D trigger;
    [SerializeField] private Collider2D platformCollider;
    private bool isbreaking=false;

    void Start()
    {
        
        anim = GetComponentInChildren<Animator>();
       
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
            if (isbreaking==false && other.CompareTag("Player"))
            {
            StartCoroutine(BreakSequence());
            isbreaking = true;
            }
    }

    IEnumerator BreakSequence()
    {
        trigger.enabled = false;
        
        yield return new WaitForSeconds(2f);

        platformCollider.enabled = false;
        anim.SetTrigger("Break");

        
        yield return new WaitForSeconds(3f);

        trigger.enabled = true;
        platformCollider.enabled = true;
        anim.SetTrigger("Recover");
        isbreaking=false;
    }
}