using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float patrolSpeed = 2f;
    [SerializeField] private float chaseSpeed = 4f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private OldInputController player;
    [SerializeField] private float knockbackForce = 6f;
    [SerializeField] private float knockbackDuration = 0.2f;

    private int currentPatrolIndex = 0;
    private Rigidbody2D rb;
    private Vector3 baseScale;
    private SpriteRenderer spriteRenderer;
    private int health = 3;
    private bool isKnockedBack;
    private Vector2 knockPackVel;

    private enum State { Patrolling, Chasing }
    private State currentState = State.Patrolling;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        baseScale = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isKnockedBack)
            return;

        switch (currentState)
        {
            case State.Patrolling:
                Patrol();
                break;

            case State.Chasing:
                Chase();
                break;
        }
    }

    void FixedUpdate()
    {
        if (isKnockedBack)
            return;

        rb.linearVelocity = knockPackVel;
    }

    private void Patrol()
    {
        Vector2 direction = (patrolPoints[currentPatrolIndex].position - transform.position).normalized;
        knockPackVel = direction * patrolSpeed;
        FaceDirection(direction);

        if (Vector2.Distance(transform.position, player.transform.position) < detectionRange)
            currentState = State.Chasing;
    }

    private void Chase()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        knockPackVel = direction * chaseSpeed;
        FaceDirection(direction);

        if (Vector2.Distance(transform.position, player.transform.position) > detectionRange)
            currentState = State.Patrolling;
    }

    private void FaceDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.01f)
            return;

        float sign = Mathf.Sign(direction.x);
        transform.localScale = new Vector3(
            Mathf.Abs(baseScale.x) * sign,
            baseScale.y,
            baseScale.z
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            if (other.transform == patrolPoints[i])
            {
                currentPatrolIndex = (i + 1) % patrolPoints.Length;
                break;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            player.TakeDamage(1, transform.position);

            Vector2 knockDir = (transform.position - player.transform.position).normalized;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockDir * knockbackForce, ForceMode2D.Impulse);

            StartCoroutine(KnockbackCooldown());
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            if (health == 1)
            {
                Destroy(gameObject);
                return;
            }

            spriteRenderer.color = Color.red;
            health--;
            StartCoroutine(ResetColorAfterDelay(0.3f));
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator KnockbackCooldown()
    {
        isKnockedBack = true;
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }

    private IEnumerator ResetColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        spriteRenderer.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
