using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
        
    }
   

    public override void Enter()
    {
        base.Enter();
        player.SetVelocity(0,rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

   
    public override void Update()
    {
        base.Update();
        AllowJump();
        AttackCheck();
        if (!player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.airState);
        }
        HurtCheck();
    }

    

}
   