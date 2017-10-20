using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class UIMessageArgs
{
    //关联消息名称
    public string Name;
    //关联消息方法
    public Action<object> CallBack;
}
