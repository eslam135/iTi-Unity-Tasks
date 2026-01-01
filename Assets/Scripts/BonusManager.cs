using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPositions;
    [SerializeField] List<Transform> endPositions;
    [SerializeField] List<GameObject> carsToSpawn;
    [SerializeField] float coolDownMax = 5f;
    [SerializeField] float coolDownMin = 1.0f;

    private Dictionary<Transform, float> spawnerTimers = new Dictionary<Transform, float>();
    private Dictionary<GameObject, Transform> carTargets = new Dictionary<GameObject, Transform>();

    void Start()
    {
        foreach (var spawnPos in spawnPositions)
        {
           float x =  Random.Range(coolDownMin, coolDownMax);
            spawnerTimers[spawnPos] = x;
        }
    }

    void Update()
    {
        foreach (var spawnPos in spawnPositions)
        {
            spawnerTimers[spawnPos] -= Time.deltaTime;

            if (spawnerTimers[spawnPos] <= 0f)
            {
                SpawnCar(spawnPos);
                float x = Random.Range(coolDownMin, coolDownMax);
                spawnerTimers[spawnPos] = x;
            }
        }

        foreach (var car in new List<GameObject>(carTargets.Keys))
        {
            if (car == null)
            {
                carTargets.Remove(car);
                continue;
            }

            MoveCarTowardsTarget(car);
        }
    }

    private void SpawnCar(Transform spawnPosition)
    {
        if (carsToSpawn.Count == 0 || spawnPositions.Count != endPositions.Count) return;

        int spawnIndex = spawnPositions.IndexOf(spawnPosition);
        if (spawnIndex >= endPositions.Count) return;
        Transform endPosition = endPositions[spawnIndex];

        GameObject carPrefab = carsToSpawn[Random.Range(0, carsToSpawn.Count)];

        GameObject carInstance = Instantiate(carPrefab, spawnPosition.position, spawnPosition.rotation);

        carTargets[carInstance] = endPosition;

        float moveSpeed = 10f; 
        carInstance.GetComponent<Rigidbody>().linearVelocity = (endPosition.position - spawnPosition.position).normalized * moveSpeed;
    }

    private void MoveCarTowardsTarget(GameObject car)
    {
        if (!carTargets.ContainsKey(car)) return;

        Transform target = carTargets[car];
        Vector3 direction = (target.position - car.transform.position).normalized;

        Rigidbody rb = car.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float moveSpeed = 10f;
            rb.linearVelocity = direction * moveSpeed;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                car.transform.rotation = Quaternion.Slerp(car.transform.rotation, targetRotation, Time.deltaTime * 5f);
            }
        }
    }
}
