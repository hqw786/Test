using System;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    public int ID { get; private set; }//阶段序号
    public string Name { get; private set; }//阶段名称
    public string[] Context { get; private set; }//阶段内容
    public GameObject MainModel { get; private set; }//主模型
    public GameObject StartModel { get; private set; }//前过渡模型
    public GameObject EndModel { get; private set; }//后过渡模型
    public string Icon { get; private set; }//图标或图片
    public bool isLock;//是否锁定
    //public 
}