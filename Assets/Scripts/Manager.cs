using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;


    void Start()
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        playerPrefab.gameObject.name = "Player";

    }

    void Update()
    {



    }

}
