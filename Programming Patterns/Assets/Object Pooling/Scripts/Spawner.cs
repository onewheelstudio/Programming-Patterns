using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn = 5f;
    private float timeSinceSpawn;
    private ObjectPool_Simple objectPool;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool_Simple>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if(timeSinceSpawn >= timeToSpawn)
        {
            GameObject newCritter = objectPool.GetCritter();
            newCritter.transform.position = this.transform.position;
            timeSinceSpawn = 0f;
        }
    }
}
