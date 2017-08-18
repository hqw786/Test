using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum MainGameStatus
{
    StartUp,
    Menu,
    Play,
    End,
    Exit
}
public static class Consts
{
    public static readonly string XMLPath = "Config";//XML配置文件存放位置（Resources/Config/*.xml)
    //C
    public const string C_StartUp = "命令-启动";
    public const string C_ShowCompanyLogo = "命令-显示公司Logo";
    public const string C_EnterScene = "命令-进入场景";
    public const string C_ExitScene = "命令-退出场景";
    public const string C_StartLevel = "命令-开始关卡";
    public const string C_EndLevel = "命令-结束关卡";
    public const string C_SelectMenu = "命令_选择菜单";
    //V
    public const string V_StartLogo = "视图-开始Logo";
    public const string V_ShowCompanyLogo = "视图-其他公司Logo";
    public const string V_SelectMenu = "视图_选择菜单";
    //M
    public const string M_StageModel = "模型-阶段";
}
