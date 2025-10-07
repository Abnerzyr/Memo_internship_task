using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine { get; private set; }
    public EnemyDeadState deadState { get; private set; }
    public EnemyMoveState moveState { get; private set; }

    public float Movespeed;
    public float deadForce;
    protected override void Awake()
    {
        
        base.Awake();
        stateMachine = new EnemyStateMachine();
        deadState = new EnemyDeadState(this, stateMachine, "Dead");
        moveState = new EnemyMoveState(this, stateMachine, "Move");
    }

    protected override void Start()
    {
        base.Start();
        
        
    }

    protected override void Update()
    {
        stateMachine.currentState.Update();
        base.Update();
    }
    public void Die()
    {
        stateMachine.ChangeState(deadState);
    }
}
    
