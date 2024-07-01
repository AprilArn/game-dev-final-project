using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public GameObject cubePrefab;
    public float spawnInterval = 6.5f;

    void Start()
    {

        InvokeRepeating( "SpawnCube", spawnInterval, spawnInterval );

    }

    void SpawnCube()
    {

        Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 20, Random.Range(-10, 11));
        Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);

    }

}

