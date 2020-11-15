using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveTurnBased : MonoBehaviour
{
    [SerializeField]
    private List<Move> commandList = new List<Move>();

    #region path drawing
    //path drawing
    public int index = 0;
    private float verticalOffset = 0.1f;
    private Vector3 startPoint;
    private PathDraw pathDrawer;


    private void Start()
    {
        startPoint = this.transform.position;
        startPoint.y = verticalOffset;
        pathDrawer = this.GetComponent<PathDraw>();
    }
    #endregion

    public void AddCommand(ICommand command)
    {
        commandList.Add(command as Move);
    }

    public void DoMoves()
    {
        StartCoroutine(DoMovesOverTime());
    }

    private IEnumerator DoMovesOverTime()
    {
        foreach (Move move in commandList)
        {
            move.Execute();

            UpdateLine();
            index++; //used for path drawing

            yield return new WaitForSeconds(0.5f);
        }

        index = 0;
    }

    public void UpdateLine()
    {
        if (pathDrawer == null)
            return;

        List<Vector3> positions = new List<Vector3>();
        positions.Add(startPoint);

        for (int i = 0; i < index; i++)
        {
            Vector3 newPosition = commandList[i].GetMove() + positions[i];
            newPosition.y = verticalOffset; // used to keep it near the ground
            positions.Add(newPosition);
        }

        pathDrawer.UpdateLine(positions);
    }
}

