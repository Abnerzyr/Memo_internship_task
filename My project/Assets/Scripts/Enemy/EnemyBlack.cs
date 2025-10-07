using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBlack : Enemy
{
    public float jumpForce;
    public float jumpCooldown;
    public BlackJumpState jumpState;
    private float jumpTimer;


    protected override void Awake()
    {
        base.Awake();
        jumpState = new BlackJumpState(this, stateMachine, "Jump");
    }

    protected override void Start()
    {
        jumpTimer=jumpCooldown;
        base.Start();
        stateMachine.Initialize(moveState);
        
    }

    protected override void Update()
    {
        base.Update();
        jumpTimer-=Time.deltaTime;
        if (IsGroundDetected() && jumpTimer < 0&&stateMachine.currentState!=deadState) { 
            stateMachine.ChangeState(jumpState);
            jumpTimer=jumpCooldown;
        }
    }
}
