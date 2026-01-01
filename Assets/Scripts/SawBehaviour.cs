using UnityEngine;

public class SawBehaviour : MonoBehaviour
{
    Rigidbody2D rb;
    public float rotationSpeed = 500f; 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(rotationSpeed * Time.fixedDeltaTime);
    }
}