using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackJumpState : EnemyState
{
    private EnemyBlack enemyBlack;
    public BlackJumpState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
        enemyBlack=_enemy as EnemyBlack;
    }
    

    public override void Enter()
    {
        base.Enter();
        enemy.rb.velocity = new Vector2(enemy.rb.velocity.x,enemyBlack.jumpForce);
        
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if (enemy.IsGroundDetected() && enemy.rb.velocity.y<=0)
        {
            stateMachine.ChangeState(enemyBlack.moveState);
        }
    }
}

