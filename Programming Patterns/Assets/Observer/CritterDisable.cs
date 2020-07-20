using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterDisable : MonoBehaviour
{
    public delegate void CritterKill();
    public static CritterKill critterKill;

    private void OnDisable()
    {
        critterKill?.Invoke();
    }
}
