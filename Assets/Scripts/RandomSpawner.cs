// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class RandomSpawner : MonoBehaviour
// {
//     public GameObject cubePrefab;

//     // Update is called once per frame
//     void Update()
//     {

//         if (  Input.GetKeyDown(KeyCode.Space) ) {

//             Vector3 raandomSpawnPosition = new Vector3( Random.Range(-15,16 ), 5, Random.Range(-15,16) );
//             Instantiate( cubePrefab, raandomSpawnPosition, Quaternion.identity );

//         }

//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public float spawnInterval = 5f;

    void Start()
    {
        InvokeRepeating("SpawnCube", spawnInterval, spawnInterval);
    }

    void SpawnCube()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), 20, Random.Range(-10, 11));
        Instantiate(cubePrefab, randomSpawnPosition, Quaternion.identity);
    }
}

