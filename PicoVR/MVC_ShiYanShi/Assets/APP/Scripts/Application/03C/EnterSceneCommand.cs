using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class EnterSceneCommand : Control, ICommand
{
    //进入新场景注册视图的都放到这边
    public void Execute(object data)
    {
        Debug.Log("EnterSceneCommand.Execute:  ");
        SceneArgs e = data as SceneArgs;
        switch(e.sceneIndex)
        {
            case 1://启动，显示自己公司LOGO
                Debug.Log("关心命令");
                RegisterView(GameObject.Find("/TitlesLogo/Logo").GetComponent<LauchView>());
                break;
            case 2://显示对面LOGO或标题，如有多的步骤，可增加一个场景，或多加一个显示过程
                RegisterView(GameObject.Find("/Canvas/LogoPanel").GetComponent<ShowCompanyLogoView>());
                break;
            case 3://正常关卡
                {
                    RegisterView(GameObject.Find("/shiyanshi/Menu").GetComponent<SelectMenuView>());
                }
                break;
            case 4://结束
                {

                }
                break;
        }
    }
}
