using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;

    [SerializeField] private Vector2 maxMinSpawnZ;
    private void Start()
    {
        InvokeRepeating("SpawnObstacle", 0, 0.5f);
    }

    private void SpawnObstacle()
    {
        Vector3 obstacleSpawnPoint = new Vector3(-50, 1, Random.Range(maxMinSpawnZ.x, maxMinSpawnZ.y));


        GameObject obstacleInstance = Instantiate(obstaclePrefab);

        obstacleInstance.transform.position = obstacleSpawnPoint;
    }
}
