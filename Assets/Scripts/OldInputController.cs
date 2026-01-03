using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OldInputController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float knockbackForce = 6f;
    [SerializeField] private float knockbackDuration = 0.2f;

    private Animator anim;
    private Rigidbody2D rb;
    private float moveX;
    private bool facingLeft;
    private bool isKnockedBack;
    private Vector2 desiredVelocity;

    int collectedCoins = 0;
    int health = 10;

    public Transform groundCheck;
    public float groundDistance = 0.2f;
    [SerializeField] public LayerMask groundMask;
    public bool isGrounded;

    public Transform leftWallCheck;
    public Transform rightWallCheck;
    public float wallCheckDistance = 0.2f;
    [SerializeField] public LayerMask wallMask;
    private bool isTouchingLeftWall;
    private bool isTouchingRightWall;
    private bool isTouchingWall;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGround();
        CheckWalls();

        if (isKnockedBack)
            return;

        moveX = Input.GetAxis("Horizontal");

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            anim.SetTrigger("Jump");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();
        }

        HandleFacing();
    }

    void FixedUpdate()
    {
        if (isKnockedBack)
            return;

        if (isTouchingWall && !isGrounded)
        {
            desiredVelocity = new Vector2(0, rb.linearVelocity.y);
        }
        else
        {
            desiredVelocity = new Vector2(moveX * speed, rb.linearVelocity.y);
        }

        rb.linearVelocity = desiredVelocity;

        anim.SetBool("isRunning", moveX != 0 && !isTouchingWall);
    }

    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);
    }

    private void CheckWalls()
    {
        isTouchingLeftWall = Physics2D.OverlapCircle(leftWallCheck.position, wallCheckDistance, wallMask);
        isTouchingRightWall = Physics2D.OverlapCircle(rightWallCheck.position, wallCheckDistance, wallMask);

        if (moveX < 0 && isTouchingLeftWall)
        {
            isTouchingWall = true;
        }
        else if (moveX > 0 && isTouchingRightWall)
        {
            isTouchingWall = true;
        }
        else
        {
            isTouchingWall = false;
        }
    }

    private void HandleFacing()
    {
        if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            facingLeft = true;
        }
        else if (moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            facingLeft = false;
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody2D bulletrb = bullet.GetComponent<Rigidbody2D>();
        bulletrb.linearVelocity = shootPoint.right * projectileSpeed * (facingLeft ? -1 : 1);
        bulletrb.AddForce(Vector2.up * 6f, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collectable>() != null)
        {
            collectedCoins++;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("EdgeOfTheWorld"))
        {
            Die();
        }
    }

    public void TakeDamage(int damage, Vector2 damageSourcePosition)
    {
        health -= damage;

        Vector2 knockDir = (transform.position - (Vector3)damageSourcePosition).normalized;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(KnockbackCooldown());

        if (health <= 0)
            Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            TakeDamage(1, collision.gameObject.transform.position);
        }
    }
    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private IEnumerator KnockbackCooldown()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }
}