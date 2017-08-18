using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class EnterSceneCommand : Control, ICommand
{
    //进入新场景注册视图的都放到这边
    public void Execute(object data)
    {
        SceneArgs e = data as SceneArgs;
        switch(e.sceneIndex)
        {
            case 0://启动，显示自己公司LOGO

                break;
            case 1://显示对面LOGO或标题，如有多的步骤，可增加一个场景，或多加一个显示过程
                break;
            case 2://正常关卡
                break;
            case 3://结束
                break;
        }
    }
}
