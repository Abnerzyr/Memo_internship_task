using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    protected Player player;
    [Header("Attack info")]
    [SerializeField]private GameObject arrowPrefab;
    [SerializeField] private float arrowGravity;
    [SerializeField] private float arrowSpeed = 10;
    private  Vector2 spawanplace;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        arrowPrefab = Resources.Load<GameObject>("Arrow");
    }

    public void CreateArrow()
    {
        if (arrowPrefab == null) return;
        spawanplace= new Vector2(player.transform.position.x + player.facingD * 0.5f, player.transform.position.y);
        GameObject newArrow = Instantiate(arrowPrefab, spawanplace, transform.rotation);
        Arrow newArrowScript = newArrow.GetComponent<Arrow>();
        newArrowScript.SetupArrow(new Vector2(player.facingD * arrowSpeed, 0), arrowGravity);
        AudioManager.Instance.PlaySFX(0);
    }
}