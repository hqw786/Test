﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public enum RoamView
{
    fix,custom
}
public enum MapType
{
    normal,mine
}
public enum WarpName
{
    江苏科技大厦,
    五台山体育场入口,
    脑科医院,
    广州路227号
}
public enum RoamStatus
{
    none,
    roaming,
    pause
}
public class ConfigData : MonoBehaviour {
    public static ConfigData Instance;
    public const float fadeTime = 1.5f;
    public const float scaleTime = 1.5f;

    [HideInInspector]
    public List<Transform> roamPath = new List<Transform>();

    public List<NodeInfo> pathNodeInfo = new List<NodeInfo>(); 
    
    // Use this for initialization
    void Awake()
    {
        Instance = this;
        //TODO:读取XML文件并保存;
        //读取XML文件成流，格式化成XMLDocdment再解析
        GetXmlData();
    }
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void GetXmlData()
    {
        TextAsset ta = Resources.Load<TextAsset>("Config/gzlmap");
        string x = ta.ToString();
        XmlDocument xd = new XmlDocument();
        xd.LoadXml(x);
    }
}
