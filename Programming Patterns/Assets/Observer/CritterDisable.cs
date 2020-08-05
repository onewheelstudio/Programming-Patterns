using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CritterDisable : MonoBehaviour
{
    public static event Action critterKill;

    private void OnDisable()
    {
        critterKill?.Invoke();
    }
}



