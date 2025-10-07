using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyState : PlayerState
{
    public PlayerFlyState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.buffTime;
        player.rb.gravityScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();
        player.rb.gravityScale = 3f;

    }

    public override void Update()
    {
        base.Update();
        if(MySceneManager.isGameOver) { return; }
        player.SetVelocity(player.moveSpeed * xInput,player.flySpeed*yInput);
        if (stateTimer <= 0f)
        {
            stateMachine.ChangeState(player.airState);
        }
        
    }
}
