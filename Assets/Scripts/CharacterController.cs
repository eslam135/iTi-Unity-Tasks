using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    //Rigidbody2D rb;
    //Animator anim;
    //[SerializeField] InputActionAsset inputActions;
    //[SerializeField] private Camera camera;
    //private InputAction moveAction;
    //private InputAction jumpAction;
    //private Vector2 moveAmount;
    //private Vector2 jumpAmount;
    //private float moveSpeed = 5f;
    //private float jumpForce = 7f;
    
    private void OnEnable()
    {
        //inputActions.FindActionMap("Player").Enable();
    }

    private void OnDisable()
    {
        //inputActions.FindActionMap("Player").Disable();
    }


    void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        //camera = Camera.main;
        //moveAction = inputActions.FindAction("Move");
        //jumpAction = inputActions.FindAction("Jump");
    }

    void Update()
    {
        //moveAmount = moveAction.ReadValue<Vector2>();
        //if (jumpAction.triggered)
        //{
        //    Jump();
        //}
    }
    private void FixedUpdate()
    {
        //Move();
    }
    private void Jump()
    {
        //if (rb != null)
        //{
        //    rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        //    anim.Play("Jump");
        //}
    }
    private void LateUpdate()
    {
        //if (camera != null)
        //{
        //    Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z);
        //    camera.transform.position = newPosition;
        //}
    }
    private void Move()
    {
        //Debug.Log("MoveAmount: " + moveAmount);
        //if(moveAmount.x > 0)
        //{
        //    transform.localScale = new Vector3(1, 1, 1);
        //}
        //else if (moveAmount.x < 0)
        //{
        //    transform.localScale = new Vector3(-1, 1, 1);
        //}

        //if (rb != null)
        //{
        //    Vector2 movement = new Vector2(moveAmount.x * moveSpeed, rb.linearVelocity.y);
        //    rb.linearVelocity = movement;
        //    if (movement.x != 0)
        //    {
        //        anim.SetBool("isRunning", true);    
        //    }
        //    else
        //    {
        //        anim.SetBool("isRunning", false);

        //    }
        //}
    }
}
