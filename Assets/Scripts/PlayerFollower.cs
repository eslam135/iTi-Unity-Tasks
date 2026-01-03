using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(1, 2, -10);
    [SerializeField] private Vector2 clamVals = new Vector2(0.97f, 1.9f);
    [SerializeField] private float xClamp = 0;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }
    }
    private void FixedUpdate()
    {
        float x = (player.position.x + offset.x);
        float y = player.position.y + offset.y;
        x = Mathf.Clamp(x, xClamp, float.MaxValue);
        y = Mathf.Clamp(y, clamVals.x, clamVals.y);
        transform.position = new Vector3 (x,y , -10);
    }
}
