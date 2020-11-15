using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler
{
    //the ability to draw the path has been removed as it's not generic
    //by making these variables public or allowing access to their values
    //it would be possible restore the path drawing functionality

    private List<ICommand> commandList = new List<ICommand>();
    private int index;

    public void AddCommand(ICommand command)
    {
        if (index < commandList.Count)
            commandList.RemoveRange(index, commandList.Count - index);

        commandList.Add(command);
        command.Execute();
        index++;
    }

    public void UndoCommand()
    {
        if (commandList.Count == 0)
            return;
        if (index > 0)
        {
            commandList[index - 1].Undo();
            index--;
        }
    }

    public void RedoCommand()
    {
        if (commandList.Count == 0)
            return;

        if (index < commandList.Count)
        {
            index++;
            commandList[index - 1].Execute();
        }
    }
}



