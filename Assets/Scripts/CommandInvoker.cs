using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker
{
    private readonly List<ICommand> _commandHistory = new List<ICommand>();
    private static int _counter = 0;

    public void AddCommand(ICommand command)
    {
        if (_counter < _commandHistory.Count)
        {
            _commandHistory.RemoveRange(_counter, _commandHistory.Count - _counter);
            _counter = _commandHistory.Count;
        }

        _commandHistory.Add(command);
        _counter++;
    }

    public void Execute()
    {
        if (_counter == 0)
        {
            return;
        }

        for (var i = 0; i < _counter; i++)
        {
            _commandHistory[i].Execute();
        }
    }

    public Boolean TryUndoCommand()
    {
        if (_counter == 0)
        {
            return false;
        }

        _counter--;
        return true;
    }

    public Boolean TryRedoCommand()
    {
        if (_counter < _commandHistory.Count)
        {
            _counter++;
            return true;
        }

        return false;
    }
}