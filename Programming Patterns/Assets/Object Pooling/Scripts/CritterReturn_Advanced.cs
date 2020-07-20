using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterReturn_Advanced : MonoBehaviour
{
    private ObjectPool_Advanced objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool_Advanced>();
    }

    private void OnDisable()
    {
        if(objectPool!= null)
            objectPool.ReturnGameObject(this.gameObject);
    }
}