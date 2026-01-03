using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    [SerializeField] int collectedCoins = 0;
    [SerializeField] GameObject AreaEffector;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.zero;
        }   
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Collectable>() != null)
        {
            collectedCoins++;
            Destroy(collision.gameObject);
            if (collectedCoins == 20)
            {
                AreaEffector.SetActive(false);
                ChestBehaviour.canOpen = true;
            }
        }
    }


}
