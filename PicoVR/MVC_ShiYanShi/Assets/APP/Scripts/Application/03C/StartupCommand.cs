using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//启动类，相当于初始化（只使用一次）
public class StartUpCommand : Controller, ICommand
{
    public void Execute(object data)
    {
        //注册模型（所有的模型在此注册，或者所有的基础数据在这边注册，以便使用）
        RegisterModel(new StageModel());
        //注册命令（所有的命令都要注册）
        RegisterController(Consts.C_ShowCompanyLogo,new ShowCompanyLogoCommand());
        //注册视图（命令要用到的视图在此注册）
        RegisterView(GameObject.Find("/TitlesLogo/Logo").GetComponent<LauchView>());
        //初始化
        StageModel stageModel = GetModel<StageModel>();
        stageModel.Init();
    }
}