using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class SelectMenuCommand : Control, ICommand
{

    public void Execute(object data)
    {
        Debug.Log("SelectMenuCommand.Execute:  ");
    }
}
