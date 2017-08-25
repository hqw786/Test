﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MainGameStatus
{
    showMyLogo,
    showCompanyLogo,
    title,
    menu,
    level,
    end
}
public static class Consts
{
    //对象池预置体的名称
    public const string prefabName = "";

    //音乐文件作用与对应名称
    public const string bgMusic = "";

    //M
    public const string M_ = "";

    //V
    //public const string V_Startup = "V_Startup";
    public const string V_ShowMyLogo = "V_ShowMyLogo";
    public const string V_ShowCompanyLogo = "V_ShowCompanyLogo";
    public const string V_ShowTitle = "V_ShowTitle";
    public const string V_SelectMenu = "V_SelectMenu";
    public const string V_SpawnPoints = "V_SpawnPoints";
    public const string V_EquipmentStartup = "V_EquipmentStartup";
    //C
    public const string C_Startup = "命令-启动（Init界面）";
    public const string C_EnterScene = "命令-进入场景";
    public const string C_ExitScene = "命令-退出场景";
    public const string C_ShowMyLogo = "命令-显示自己的LOGO";
    public const string C_ShowCompanyLogo = "命令-显示别人的LOGO";
    public const string C_ShowTitle = "命令-显示标题";
    public const string C_SelectMenu = "命令-选择菜单";
    public const string C_SpawnEgg = "命令-生成鸡蛋";
    public const string C_EquipmentSatrtup = "命令-设备启动";
    public const string C_EquipmentStop = "命令-设备停止";
}
