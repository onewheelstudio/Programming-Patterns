using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
    [SerializeField]
    private LineRenderer line;


    public void UpdateLine(List<Vector3> points)
    {
        line.positionCount = 0;
        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }
}
