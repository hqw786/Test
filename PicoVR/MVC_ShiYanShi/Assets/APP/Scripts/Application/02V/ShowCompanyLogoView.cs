using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class ShowCompanyLogoView : BaseView
{

    public override void HandleEvent(string eventName, object data)
    {
        
    }

    public override string Name
    {
        get { return  Consts.V_ShowCompanyLogo; }
    }
}
