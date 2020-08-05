using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CritterReturn : MonoBehaviour
{
    private ObjectPool_Simple objectPool;

    private void Start()
    {
        objectPool = FindObjectOfType<ObjectPool_Simple>();
    }

    private void OnDisable()
    {
        if(objectPool != null)
            objectPool.ReturnCritter(this.gameObject);
    }
}