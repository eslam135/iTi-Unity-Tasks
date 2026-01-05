using UnityEngine;
using UnityEngine.UI;

public class ScrollableInventory : MonoBehaviour
{
    [SerializeField] private int numOfObjects;
    [SerializeField] private GameObject image; 
    void OnEnable()
    {   
        for(int i = 0; i < numOfObjects; ++i)
        {
            Instantiate(image, Vector2.zero, Quaternion.identity, gameObject.transform);
        }
    }
}
