using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D bd;
    private Player player;
    private Entity entity;
    private bool isStuck;
    private bool isFlashing;
    private float teleportCooldownTimer = 0f;
    private const float teleportCooldown = 0.15f;
    [SerializeField] private float arrowTimer = 5;
    [SerializeField] private float stuckTimer = 7;
    [SerializeField] private float flashTime = 3;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bd = GetComponent<BoxCollider2D>();
        anim = GetComponentInChildren<Animator>();
        player = FindObjectOfType<Player>();
        isFlashing = false;



    }
    private void Start()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

    }

    public void SetupArrow(Vector2 _dir, float _gravityScale)
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
        if (bd == null)
            bd = GetComponent<BoxCollider2D>();
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        if (_dir.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    private void Update()
    {
        arrowTimer -= Time.deltaTime;
        teleportCooldownTimer-= Time.deltaTime;

        if (!isStuck && arrowTimer < 0)
        {
            Destroy(gameObject);
        }

        if (isStuck)
        {
            stuckTimer -= Time.deltaTime;
        }
        if (stuckTimer < 0)
        {
            Destroy(gameObject);
        }
        else if (stuckTimer <= flashTime && !isFlashing)
        {
            isFlashing = true;
            Flash(flashTime);
        }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isStuck)
        {
            Destroy(gameObject);

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Die();
            }

            Destroy(gameObject);
            return;
        }

        if (!isStuck && collision.gameObject.layer==3)
        {
            StickToSurface();
        }
    }

    private void StickToSurface()
    {
        isStuck = true;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0;


        gameObject.layer = LayerMask.NameToLayer("Platform");


        PlatformEffector2D effector = gameObject.AddComponent<PlatformEffector2D>();
        effector.useColliderMask = false;






    }
    public void Flash(float flashTime)
    {
        StartCoroutine(FlashCoroutine());

        IEnumerator FlashCoroutine()
        {

            SpriteRenderer sr = GetComponent<SpriteRenderer>();

            for (int i = 10; i > 0; i--)
            {
                sr.enabled = !sr.enabled;

                yield return new WaitForSeconds(i * flashTime / 55f);

            }
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.CompareTag("BoundaryL"))
            TeleportToBoundary("R");
        else if (collision.CompareTag("BoundaryR"))
            TeleportToBoundary("L");
        else if (collision.CompareTag("BoundaryT"))
            TeleportToBoundary("B");
        else if (collision.CompareTag("BoundaryB"))
            TeleportToBoundary("T");

        
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
}
