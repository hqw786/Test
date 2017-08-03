using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoamNodeInfo
{
    start,
    main,//主要
    assist,//辅助,用于转向的辅助点,不用在地图上显示出来。
    speed,//加速，比辅助点多个加速功能。
    end
}
public enum RoamNodeName
{
    某某位置1,
    某某位置2,
    某某位置3,
    某某位置4,
}
public class RoamInfo : MonoBehaviour {
    public RoamNodeInfo nodeInfo;
    [HideInInspector]
    public RoamNodeName nodeName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
