using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    float nextSpawnTime = 0;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nextSpawnTime < Time.timeSinceLevelLoad) {
            GameObject newProjectile = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
            nextSpawnTime = Time.timeSinceLevelLoad + (-Mathf.Log10(Time.timeSinceLevelLoad/5) + 2);
        }
    }
}
