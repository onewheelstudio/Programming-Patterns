using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Simple : MonoBehaviour
{
    [SerializeField]
    private GameObject critterPrefab;
    [SerializeField]
    private Queue<GameObject> critterPool = new Queue<GameObject>();
    [SerializeField]
    private int poolStartSize = 5;

    private void Start()
    {
        for (int i = 0; i < poolStartSize; i++)
        {
            GameObject critter = Instantiate(critterPrefab);
            critterPool.Enqueue(critter);
            critter.SetActive(false);
        }
    }

    public GameObject GetCritter()
    {
        if (critterPool.Count > 0)
        {
            GameObject critter = critterPool.Dequeue();
            critter.SetActive(true);
            return critter;
        }
        else
        {
            GameObject critter = Instantiate(critterPrefab);
            return critter;
        }
    }

    public void ReturnCritter(GameObject critter)
    {
        critterPool.Enqueue(critter);
        critter.SetActive(false);
    }
}
