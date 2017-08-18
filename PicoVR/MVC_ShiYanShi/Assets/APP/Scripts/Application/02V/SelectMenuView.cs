using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


class SelectMenuView : BaseView
{

    public override string Name
    {
        get { return Consts.V_SelectMenu; }
    }

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    public override void RegisterEvents()
    {
        AttentionEvents.Add(Consts.V_SelectMenu);
    }
}
