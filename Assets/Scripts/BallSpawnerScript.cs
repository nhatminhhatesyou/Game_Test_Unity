using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawnerScript : MonoBehaviour
{
    float timer = 0;
    public GameObject ballPrefab;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 1)
        {
            timer = 0;
            BallSpawner();
        }
    }

    void BallSpawner()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-7, 8), 2, Random.Range(-5, 9));
        Instantiate(ballPrefab, randomSpawnPosition, Quaternion.identity);
    }
}
