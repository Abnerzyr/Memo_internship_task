using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDeadState : EnemyState
{
    public EnemyDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        enemy.rb.velocity = new Vector3(0, enemy.deadForce);
        Transform[] allChildren = enemy.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.layer = LayerMask.NameToLayer("DeadEntity");
            
        }
        MySceneManager.Instance.OnEnemyDefeated();
        ItemsManager.Instance.SpawnDrop(enemy.transform.position);
        AudioManager.Instance.PlaySFX(4);
        base.Enter();
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
