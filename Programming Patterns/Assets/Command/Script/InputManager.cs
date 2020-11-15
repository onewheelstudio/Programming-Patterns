using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Button up;
    [SerializeField]
    private Button down;
    [SerializeField]
    private Button left;
    [SerializeField]
    private Button right;

    [SerializeField]
    private Button undo;
    [SerializeField]
    private Button redo;

    [SerializeField]
    private CharacterMove character;
    [SerializeField]
    private CharacterMoveUndo characterUndo;
    [SerializeField]
    UICommandList uiCommandList;
    [SerializeField]
    CharacterMoveClean characterCleanMove;

    private void OnEnable()
    {
        up?.onClick.AddListener(() => SendMoveCommand(character.transform, Vector3.forward, 1f));
        down?.onClick.AddListener(() => SendMoveCommand(character.transform, Vector3.back, 1f));
        left?.onClick.AddListener(() => SendMoveCommand(character.transform, Vector3.left, 1f));
        right?.onClick.AddListener(() => SendMoveCommand(character.transform, Vector3.right, 1f));

        undo?.onClick.AddListener(() => character.UndoCommand());
        redo?.onClick.AddListener(() => character.RedoCommand());
    }

    private void SendMoveCommand(Transform objectToMove, Vector3 direction, float distance)
    {
        ICommand movement = new Move(objectToMove, direction, distance);
        character.AddCommand(movement as Move);
        uiCommandList?.AddCommand(movement);
    }
}
