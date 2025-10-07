using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerState
{
    
    public PlayerHurtState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        
    }
   
    private float retreatForce = 4f;
    private int retreatD = 1;
    private float hurtDuration = 0.4f;


    public override void Enter()
    {
        
        
        base.Enter();

        MySceneManager.AddBlood(-1);
        retreatD = -player.facingD;
        

        player.ZeroVelocity();
        
        player.rb.velocity = new Vector3(retreatD * retreatForce, player.jumpForce * 1.2f);
        stateTimer= hurtDuration;
        AudioManager.Instance.PlaySFX(7);



    }

    public override void Exit()
    {
        hurtCheck.isHurt = false;
        base.Exit();

    }

    public override void Update()
    {
        base.Update();
        if (stateTimer<0.2f)
        {
            AllowMove();
        }
        if (player.IsGroundDetected()&&stateTimer<0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}

