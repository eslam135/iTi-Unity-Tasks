using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownController : MonoBehaviour
{
    Animator anim;
    float moveX, moveY;
    [SerializeField] float moveSpeed = 5f;
    BoxCollider2D bd;
    int collectedCoins = 0;
    int health = 10;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        bd = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("attack");
        }
    }

    void FixedUpdate()
    {
        if (moveX != 0 || moveY != 0)
        {
            Move();
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            anim.SetBool("move", false);
        }
    }

    void Move()
    {
        Vector2 movement = new Vector2(moveX, moveY) * moveSpeed;
        rb.linearVelocity = movement;

        anim.SetFloat("moveX", moveX);
        anim.SetFloat("moveY", moveY);
        anim.SetBool("move", true);
    }

    public void enableCollision()
    {
        bd.enabled = true;
    }

    public void disableCollision()
    {
        bd.enabled = false;
    }

    public void TakeDamage(int damage, Vector2 damageSourcePosition)
    {
        health -= damage;

        Vector2 knockDir = (rb.position - damageSourcePosition).normalized;

        rb.linearVelocity = Vector2.zero;

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
}
