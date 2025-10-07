using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFly : Enemy
{
    


    protected override void Awake()
    {
        base.Awake();

        
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(moveState);
    }

    protected override void Update()
    {
        base.Update();
        if(stateMachine.currentState == deadState)
        {
            rb.gravityScale = 5;
        }
    }
}

