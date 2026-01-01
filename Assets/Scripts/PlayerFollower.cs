using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset = new Vector3(1, 2, -10);

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
        x = Mathf.Clamp(x, 0, float.MaxValue);
        y = Mathf.Clamp(y, 0.97f, 1.9f);
        transform.position = new Vector3 (x,y , -10);
    }
}
