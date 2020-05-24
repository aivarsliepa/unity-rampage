using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawnPrefab;
    public float startTime = 2.0f;
    public float spawnRate = 2.0f;

    void Start()
    {
        InvokeRepeating("Spawn", startTime, spawnRate);
    }

   private void Spawn() {
       Instantiate(objectToSpawnPrefab, transform.position, Quaternion.identity);
   }
}
