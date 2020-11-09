using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move : ICommand
{
    [SerializeField]
    private Vector3 direction = Vector3.zero;
    private float distance;
    private Transform objectToMove;

    public Move(Transform objectToMove, Vector3 direction, float distance = 1f)
    {
        this.direction = direction;
        this.objectToMove = objectToMove;
        this.distance = distance;
    }

    public void Execute()
    {
        objectToMove.position += direction * distance;
    }

    public void Undo()
    {
        objectToMove.position -= direction * distance;
    }

    //used to draw the path of the object
    public Vector3 GetMove()
    {
        return direction * distance;
    }
}
