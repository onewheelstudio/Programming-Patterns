using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner_Advanced : MonoBehaviour
{
    [SerializeField]
    private float timeToSpawn = 5f;
    private float timeSinceSpawn;
    private ObjectPool_Advanced objectPool;
    [SerializeField]
    private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        objectPool = FindObjectOfType<ObjectPool_Advanced>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceSpawn += Time.deltaTime;
        if (timeSinceSpawn >= timeToSpawn)
        {
            GameObject newCritter = objectPool.GetObject(prefab);
            newCritter.transform.position = this.transform.position;
            timeSinceSpawn = 0f;
        }
    }
}
