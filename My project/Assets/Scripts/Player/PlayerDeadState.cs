using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public float deathForce = 15f;
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        MySceneManager.AddBlood(-1);
        base.Enter();
        player.ZeroVelocity();
        player.SetVelocity(0, deathForce);
        player.gameObject.layer = LayerMask.NameToLayer("DeadEntity");
        AudioManager.Instance.PlaySFX(6);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
    }
}
    

