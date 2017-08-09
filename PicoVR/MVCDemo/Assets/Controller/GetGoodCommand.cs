using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GetGoodCommand : ICommand
{
    GoodsProxy goodProxy = GoodsProxy.GetIntance();
    public void Excute(INotifier inotifier)
    {

    }
}
