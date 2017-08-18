using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class SelectMenuCommand : Control, ICommand
{

    public void Execute(object data)
    {
        RegisterView(GameObject.Find("/shiyanshi/Menu").GetComponent<SelectMenuView>());

    }
}
