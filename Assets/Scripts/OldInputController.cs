using UnityEngine;

public class OldInputController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;
    private Animator anim;
    private float moveX, moveY;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
    }

    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");
        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("Jump");
        }
    }
    private void FixedUpdate()
    {
        if(moveX != 0)
        {
            Move();
        }else
        {
            anim.SetBool("isRunning", false);
        }
        if (moveY > 0)
        {
            Jump();
            moveY = 0;
        }
    }
    private void Move()
    {
        if (moveX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if(moveX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        Vector3 movement = new Vector3(moveX * speed, 0, 0) * Time.fixedDeltaTime;
        transform.position += movement;
        anim.SetBool("isRunning", true);
    }
    private void Jump()
    {
        Vector3 jump = new Vector3(0, jumpForce, 0) * Time.deltaTime;
        transform.position += jump;    
    }

}
