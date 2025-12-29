using UnityEngine;

public class FractalGenerator : MonoBehaviour
{
    [Range(1, 13)]
    [SerializeField] private int iterations = 1;
    [SerializeField] private float childScale = 0.5f;

    [SerializeField] private Color rootColor = Color.white;
    [SerializeField] private Color deepColor = Color.cyan;

    [SerializeField] private bool switchShapes = true;
    [SerializeField] private bool updateInRealTime = true;

    private int _lastIterations;

    void Start()
    {
        Generate();
    }

    void Update()
    {
        if (iterations != _lastIterations && updateInRealTime)
        {
            Generate();
        }
    }

    public void Generate()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        _lastIterations = iterations;
        CreateChild(iterations, transform, Vector3.zero, 1f);
    }

    void CreateChild(int depth, Transform parent, Vector3 localPos, float scale)
    {
        if (depth <= 0) return;


        PrimitiveType type = PrimitiveType.Cube;

        if(depth % 2 == 1 && switchShapes)
        {
            type = PrimitiveType.Sphere;
        }
        else
        {
            type = PrimitiveType.Cube;
        }


        GameObject cube = GameObject.CreatePrimitive(type);

        cube.transform.SetParent(parent, false);
        cube.transform.localPosition = localPos;
        cube.transform.localScale = Vector3.one * scale;

        float t;

        if (iterations > 1)
        {
            t = (float)(iterations - depth) / (iterations - 1);
        }
        else
        {
            t = 0f;
        }
        Renderer rend = cube.GetComponent<Renderer>();
        rend.material.color = Color.Lerp(rootColor, deepColor, t);

        Vector3[] directions = { Vector3.up, Vector3.right, Vector3.left, Vector3.forward, Vector3.back };

        foreach (Vector3 dir in directions)
        {
            float offset = 0.5f + (0.5f * childScale);
            CreateChild(depth - 1, cube.transform, dir * offset, childScale);
        }
    }
}