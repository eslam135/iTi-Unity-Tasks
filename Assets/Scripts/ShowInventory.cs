using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    [SerializeField] private GameObject inventory;
    public void OnShowPress()
    {
        inventory.SetActive(true);
        gameObject.SetActive(false);
    }
}
