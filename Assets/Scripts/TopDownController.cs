using UnityEngine;

public class TopDownController : MonoBehaviour
{
    Animator anim;
    float moveX, moveY;
    [SerializeField] float moveSpeed = 5f;
    BoxCollider2D bd;
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
            anim.SetBool("move", false);
        }
    }
    void Move()
    {
        Vector3 movement = new Vector3(moveX, moveY, 0) * moveSpeed * Time.fixedDeltaTime;
        transform.position += movement;
        
        anim.SetFloat("moveX", moveX);
        anim.SetFloat("moveY", moveY);
        anim.SetBool("move", true);
    }
    public void enableCollision()
    {
        Debug.Log("XXXXXXXXXXXXXXXXXXXXX");
        bd.enabled = true;
    }

    public void disableCollision()
    {
        bd.enabled = false;
    }
}
