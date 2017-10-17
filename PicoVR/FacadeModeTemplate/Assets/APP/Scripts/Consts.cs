using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public static class Consts
{
    //常量
    public const int SPAWN_OBJECT_NUMBER = 10;//对象池默认生成对象数量

    //对象池
    public const string Pool_XiaoGuai1 = "XiaoGuaiPrefabName";//示例：SPN_+怪物名称 = “预置体名称”;(怪物名称：可以起一个容易记的名称）
    public const string Pool_monster1 = "Capsule";
    public const string Pool_monster2 = "Cube";

    
    //音乐
    //UI界面
    public const string UI_Score = "Score";//得分
    
    #region Message 子功能调用传递的消息，MSG+子功能+具体作用

    //UI具体作用
    public const string Msg_UI_Score = "Msg_UI_统计得分";//统计得分
    //public const string Msg_Pool_GetObject = "Msg_Pool_获得对象池对象";////对象池不用事件，这里改成其他功能的消息
    
    # endregion
    
    //模板
    public const string x = "";
}
