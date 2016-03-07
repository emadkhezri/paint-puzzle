﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Code.Classes.Command
{
    public class CommandHistory
    {
        private List<ICommand> commandsList = new List<ICommand>();
        private int commandToExecuteIndex = 0;

        public void Add(ICommand command)
        {
            while (commandToExecuteIndex < commandsList.Count)
                commandsList.Remove(commandsList.Last());

            commandsList.Add(command);
            commandToExecuteIndex = commandsList.Count;
        }

        public void Undo()
        {
            if (commandToExecuteIndex == 0)
                return;

            --commandToExecuteIndex;
            commandsList[commandToExecuteIndex].Unexecute();
        }

        public void Redo()
        {
            if (commandToExecuteIndex == commandsList.Count)
                return;

            commandsList[commandToExecuteIndex].Execute();
            ++commandToExecuteIndex;
        }
    }
}