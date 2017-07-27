using System;
using System.Collections.Generic;
using UnityEngine;

public class StageInfo
{
    public int ID { get; set; }//阶段序号
    public string Name { get; set; }//阶段名称
    private List<string[]> context = new List<string[]>();
    public List<string[]> Context { get { return context; } set { context = value; } }//阶段内容
    public GameObject MainModel { get; set; }//主模型
    public GameObject StartModel { get; set; }//前过渡模型
    public GameObject EndModel { get; set; }//后过渡模型
    public string Icon { get; set; }//图标或图片
    public bool isLock;//是否锁定
    //public 
}