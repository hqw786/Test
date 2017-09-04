using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine.UI;

public class PlanetInfo : MonoBehaviour
{
    public bool isHaveSatellite;//是否有卫星
    public bool isSatellite;

    public List<Transform> satelliteList = new List<Transform>();

    public event System.Action DisplayPlanetInfoEvent;
    //TODO:在这边添加继承Model，添加功能如显示信息。
    //就不用在PlanetButton中有太多内容，执行一下就可以。
	// Use this for initialization
	void Start () 
    {

    }
    //public void AddSatellite(List<Transform> list)
    //{
    //    satelliteList = list;
    //}
}
