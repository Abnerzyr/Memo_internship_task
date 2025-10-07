using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState 
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected HurtCheckConroller hurtCheck;

    protected Rigidbody2D rb;

    protected float xInput;
    protected float yInput;


    private string animBoolName;

    protected bool triggerCalled;    
    protected float stateTimer;
    public PlayerState(Player _player, PlayerStateMachine _stateMachine,string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
        this.hurtCheck = player.GetComponentInChildren<HurtCheckConroller>();
    }
   


    public virtual void Enter()
    {
       player.anim.SetBool(animBoolName,true);
       rb = player.rb;
        triggerCalled = false;
    }
    public virtual void Update()
    {
        xInput=Input.GetAxisRaw("Horizontal");
        yInput=Input.GetAxisRaw("Vertical");
        stateTimer -= Time.deltaTime;

        player.anim.SetFloat("yVelocity",rb.velocity.y);
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName,false);
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled=true;
    }
    protected void HurtCheck()
    {
        if (hurtCheck.isHurt)
        {
            if (MySceneManager.blood > 1)
            {
                stateMachine.ChangeState(player.hurtState);
            }
            else
            {
                stateMachine.ChangeState(player.deadState);
            }

        }
    }
    protected void AttackCheck()
    {
        if (MySceneManager.isGameOver) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.attackState);
        }
    }
    protected void AllowMove()
    {
        if(MySceneManager.isGameOver) return;
        if (xInput != 0)
        {
            player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);
        }
    }
    protected void AllowJump()
    {
        if (MySceneManager.isGameOver) return;
        if (player.IsGroundDetected() && Input.GetKeyDown(KeyCode.W))
        {
            player.Jump();
        }
    }


}
