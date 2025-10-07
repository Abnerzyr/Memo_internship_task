using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAirState :PlayerState
{
    
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        
    }
    

    public override void Enter()
    {
        base.Enter();
    }


    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected())
        {

            stateMachine.ChangeState(player.idleState);

        }
        AllowMove();
        AttackCheck();
        HurtCheck();
    }

    

    public override void Exit()
    {
        base.Exit();
        

    }
}
