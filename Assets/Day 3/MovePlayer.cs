using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public float moveSpeed = 5f;

    public float mouseSensitivity = 100f;

    Rigidbody rb;
    private float xRotation = 0f;
    public UnityEngine.CharacterController controller;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<UnityEngine.CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleMouseLook();
        HandleMovement();
    }

    void HandleMovement()
    {
        float z = Input.GetAxis("Horizontal");
        float x = - Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller != null)
        {
            controller.Move(move * moveSpeed * Time.deltaTime);
        }
        else if (rb != null)
        {
            rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += move * moveSpeed * Time.deltaTime;
        }
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
