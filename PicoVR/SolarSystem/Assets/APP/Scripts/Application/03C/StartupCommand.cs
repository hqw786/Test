using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupCommand : Control
{
    public override void Execute(object data = null)
    {
        //注册M
        RegisterModel(new XmlModel());
        //注册C
        RegisterControl(Consts.C_EnterScene, typeof(EnterSceneCommand));
        RegisterControl(Consts.C_ExitScene, typeof(ExitSceneCommand));
        //RegisterControl(Consts.C_LatencyHiding, typeof(LatencyHidingCommand));
        //M初始化
        XmlModel xm = GetModel<XmlModel>();
        xm.Init();
    }
}
