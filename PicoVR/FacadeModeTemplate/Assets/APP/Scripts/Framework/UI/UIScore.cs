using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class UIScore : UIBase
{
    public override string Name
    {
        get { return "Score"; }
    }
    public override void RegisterRelateMessage(string message)
    {
        UIMessageArgs ma = new UIMessageArgs();
        ma.Name = message;
        ma.CallBack += DisplayScore;
        RelateMessage.Add(ma);
    }

    public override void HandleMessage(string message)
    {
        foreach(UIMessageArgs ma in RelateMessage)
        {
            if(ma.Name.Contains(message))
            {
                ma.CallBack();
            }
        }
    }
    void DisplayScore()
    {

    }
}