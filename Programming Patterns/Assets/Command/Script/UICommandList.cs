using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICommandList : MonoBehaviour
{
    [SerializeField]
    private List<Move> commandList = new List<Move>();
    [SerializeField]
    private Text commandText;
    public void AddCommand(ICommand command)
    {
        commandList.Add(command as Move);
        UpdateUIList();
    }

    private void UpdateUIList()
    {
        commandText.text = "Commands:";

        foreach (Move command in commandList)
        {
            commandText.text += "\n";

            Vector3 direction = command.GetMove();

            if (direction.x >= 1)
                commandText.text += "Right";
            else if (direction.x <= -1)
                commandText.text += "Left";
            else if (direction.z >= 1)
                commandText.text += "Up";
            else if (direction.z <= -1)
                commandText.text += "Down";

        }


    }
}
