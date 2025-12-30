using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f; 
    [SerializeField] private float maxAngle = 45f; 

    private Vector3 initialEuler;
    private float yawOffset = 0f; 

    void Start()
    {
        initialEuler = transform.localEulerAngles;
    }

    void Update()
    {
        float delta = 0f;

        bool pressL = Input.GetKey(KeyCode.L);
        bool pressK = Input.GetKey(KeyCode.K);

        if (pressL && !pressK)
        {
            delta = rotationSpeed * Time.deltaTime;
        }
        else if (pressK && !pressL)
        {
            delta = -rotationSpeed * Time.deltaTime;
        }

        if (delta != 0f)
        {
            yawOffset = Mathf.Clamp(yawOffset + delta, -maxAngle, maxAngle);
            Vector3 newEuler = new Vector3(initialEuler.x, initialEuler.y + yawOffset, initialEuler.z);
            transform.localEulerAngles = newEuler;
        }
    }
}
