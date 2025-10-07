using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Player : Entity
{


    [Header("BaseInfo")]
    public float moveSpeed;
    public float jumpForce;
    public float buffTime;
    public float flySpeed;


    private HurtCheckConroller hurtcheck;
    private bool isFlashing = false;
    [SerializeField] private float buffTimer;
    private string lastbuff;



    private Vector3 lastConfirmedPosition;


    #region states
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerHurtState hurtState { get; private set; }
    public PlayerDeadState deadState { get; private set; }
    public PlayerFlyState flyState { get; private set; }  
    #endregion
    protected override void Awake()
    {
        hurtcheck = GetComponentInChildren<HurtCheckConroller>();
        base.Awake();
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        airState = new PlayerAirState(this, stateMachine, "Air");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        hurtState = new PlayerHurtState(this, stateMachine, "Hurt");
        deadState = new PlayerDeadState(this, stateMachine, "Dead");
        flyState = new PlayerFlyState(this, stateMachine, "Air");





    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        buffTimer = 0f;
    }


    protected override void Update()
    {


        base.Update();
        

        
        var currentStateBeforeUpdate = stateMachine.currentState;

        if (hurtcheck.isinvincible && !isFlashing)
        {
            isFlashing = true;
            Flash(hurtcheck.invincibleTime);
        }
        if (!hurtcheck.isinvincible && isFlashing)
        {
            isFlashing = false;
        }

        if(buffTimer > 0)
        {
            buffTimer -= Time.deltaTime;
            if (buffTimer <= 0)
            {
                RemoveBuff();
                
            }
        }


        if (currentStateBeforeUpdate != stateMachine.currentState)
        {
            return;
        }
        stateMachine.currentState.Update();
        




    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        AudioManager.Instance.PlaySFX(5);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.CompareTag("Spike"))
        {
          
            stateMachine.ChangeState(hurtState);
        }
        {

            BuffCheck(collision);

        }
    }
    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Gold"))
        {
            Destroy(other);
            AudioManager.Instance.PlaySFX(3);
            MySceneManager.AddScore(100);
        }
        else if (other.CompareTag("Silver"))
        {
            Destroy(other);
            AudioManager.Instance.PlaySFX(3);
            MySceneManager.AddScore(50);
        }
        else if (other.CompareTag("Diamond"))
        {
            Destroy(other);
            AudioManager.Instance.PlaySFX(3);
            MySceneManager.AddScore(500);
        }
        
    }

    private void BuffCheck(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Buffs"))
        {

            string newbuff = collision.gameObject.tag;
            if (buffTimer > 0)
            {
                RemoveBuff();
            }
            switch (newbuff)
            {
                case "Heal":
                    MySceneManager.AddBlood(1);
                    break;
                case "Invincible":
                    hurtcheck.invincibleTime = buffTime;
                    hurtcheck.isinvincible = true;
                    break;
                case "Fly":
                    stateMachine.ChangeState(flyState);
                    break;
                case "Speed":
                    moveSpeed *= 1.5f;
                    break;
            }
            AudioManager.Instance.PlaySFX(1);
            Destroy(collision.gameObject);
            lastbuff = newbuff;
            buffTimer = buffTime;



        }
    }

    private void RemoveBuff()
    {
        if (lastbuff == null) return;
        switch (lastbuff)
        {
            case "Heal":
                break;
            case "Invincible":
                hurtcheck.invincibleTime = 0;
                hurtcheck.isinvincible = false;
                break;
            case "Fly":
                if (IsGroundDetected())
                    stateMachine.ChangeState(idleState);
                else
                    stateMachine.ChangeState(airState);
                break;
            case "Speed":
                moveSpeed /= 1.5f;
                break;
        }
        buffTimer = 0f;
        lastbuff = null;

    }
    public void Flash(float flashTime)
    {
        StartCoroutine(FlashCoroutine());

        IEnumerator FlashCoroutine()
        {

            SpriteRenderer sr = GetComponentInChildren<SpriteRenderer>();

            for (int i = 10; i > 0; i--)
            {
                if (!hurtcheck.isinvincible)
                    break;
                sr.enabled = !sr.enabled;
                yield return new WaitForSeconds(0.3f * flashTime / 10);
                sr.enabled = !sr.enabled;
                yield return new WaitForSeconds(0.7f * flashTime / 10);
            }
            sr.enabled = true;
        }
    }
}