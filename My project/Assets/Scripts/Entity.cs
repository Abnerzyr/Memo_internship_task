using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("CollisionInfo")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask wallLayer;
    [SerializeField] protected LayerMask platformLayer;

    protected float teleportColddownTimer = 0f;
    protected const float teleportColddown = 0.1f;

    #region components
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    public int facingD { get; private set; } = -1;
    protected bool facingR = false;
   
    protected virtual void Awake()
   {
        teleportColddownTimer = teleportColddown;
   }
   protected virtual void Start()
   {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
   protected virtual void Update()
   {
        if (teleportColddownTimer > 0)
            teleportColddownTimer -= Time.deltaTime;
        
       
        
    }
    #region Collision
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer|platformLayer);
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right*facingD, wallCheckDistance, wallLayer);
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x +facingD*wallCheckDistance, wallCheck.position.y));
    }
    #endregion

    #region Flip
    public void Flip() 
    {
        facingD *= -1;
        facingR = !facingR;
        transform.Rotate(0, 180, 0);
    }
    public void FlipController(float _x)
    {
        if(Time.timeScale==0||MySceneManager.isGameOver) return;
        if (_x > 0 && !facingR)
        {
            Flip();
        }
        else if (_x < 0 && facingR)
        {
            Flip();
        }
    }
    #endregion
    #region Velocity
    public void ZeroVelocity() => rb.velocity = new Vector2(0, 0);
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
        
    }


    #endregion
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (teleportColddownTimer > 0)
        {
            return;
        }


        if (collision.CompareTag("BoundaryL"))
        {
            TeleportToBoundary("R");
            teleportColddownTimer = teleportColddown;
            
        }
        else if (collision.CompareTag("BoundaryR"))
        {
            TeleportToBoundary("L");
            teleportColddownTimer = teleportColddown;
            
        }
        else if (collision.CompareTag("BoundaryT"))
        {
            TeleportToBoundary("B");
            teleportColddownTimer = teleportColddown;

        }

        else if (collision.CompareTag("BoundaryB"))
        {
            TeleportToBoundary("T");
            teleportColddownTimer = teleportColddown;

        }
        if (gameObject.layer == LayerMask.NameToLayer("DeadEntity"))
        {
           
                Destroy(gameObject);
                return;
            
        }
    }




    private void TeleportToBoundary(string boundaryMark)
    {
        GameObject boundary = GameObject.Find("Boundary" + boundaryMark);
        if (boundary == null) return;
        Vector3 pos = transform.position;


        if (boundaryMark == "L" || boundaryMark == "R")
            pos.x = boundary.transform.position.x;
        else if (boundaryMark == "T")
            pos.y = boundary.transform.position.y;

        else if (boundaryMark == "B")
            pos.y = boundary.transform.position.y;

        transform.position = pos;
    }
    
    private Vector3 lastTrackedPosition;
   
}
