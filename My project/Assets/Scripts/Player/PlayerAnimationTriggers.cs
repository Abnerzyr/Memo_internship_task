using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private AttackController attackController;
    private Player player => GetComponentInParent<Player>();
    private void Awake()
    {
        attackController = FindObjectOfType<AttackController>();

    }

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }
    private void Attack()
    {
        attackController.CreateArrow();
    }
}
