using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveUndo : MonoBehaviour
{
    [SerializeField]
    private List<Move> commandList = new List<Move>();
    public int index = 0;

    #region path drawing
    //path drawing
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
        command.Execute();

        UpdateLine();
    }

    public void UndoCommand()
    {
        if (commandList.Count == 0)
            return;

        commandList[commandList.Count - 1].Undo();
        commandList.RemoveAt(commandList.Count - 1);

        UpdateLine();
    }

    public void UpdateLine()
    {
        if (pathDrawer == null)
            return;

        List<Vector3> positions = new List<Vector3>();
        positions.Add(startPoint);

        for (int i = 0; i < commandList.Count; i++)
        {
            Vector3 newPosition = commandList[i].GetMove() + positions[i];
            newPosition.y = verticalOffset; // used to keep it near the ground
            positions.Add(newPosition);
        }

        pathDrawer.UpdateLine(positions);
    }
}


