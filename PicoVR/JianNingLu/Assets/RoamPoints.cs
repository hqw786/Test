using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamPoints : MonoBehaviour 
{
	public Transform pointLeftDown;
	public Transform pointRightUp;
	public Transform rootNode;

	public GameObject mapBG;

	//float mapW;
	//float mapH;
	Vector2 mapSize;
	//float mapOriginH;
	//float mapOriginW;
	Vector2 mapOrigin;
	//float flagH;
	//float flagV;
	Vector2 flagPosition;
	// Use this for initialization
	void Awake()
	{
		//取得UI

	}
	void SetMap()
	{
		//根据边界，决定MapBG的图片的大小
		float w = Mathf.Abs(pointRightUp.position.x - pointLeftDown.position.x);
		float h = Mathf.Abs(pointRightUp.position.z - pointLeftDown.position.x);
		if (w >= h)
		{
			mapSize = new Vector2(w, h);
		}
		else
		{
			mapSize = new Vector2(h, w);
		}

	}
	void Start()
	{
		//MapBG大小
		SetMap();
		//保存漫游点
		foreach (Transform t in transform)
		{
			ConfigData.Instance.roamPath.Add(t);
		}
		//保存漫游点路径节点信息
		GetPathNodeInfo();
	}
	void GetPathNodeInfo()
	{
		foreach (Transform t in ConfigData.Instance.roamPath)
		{
			NodeInfo ni = new NodeInfo();
			RoamInfo ri = t.GetComponent<RoamInfo>();
			if (ri.nodeInfo == RoamNodeInfo.start)
			{//起点
				ni.isStart = true;
				ni.isMain = true;
				ni.startNum = -1;
				ni.endNum = 0;
				string s = ri.nodeName.ToString();
				s = s.Substring(s.IndexOf(".") + 1);
				ni.showContext = s;
			}
			else if (ri.nodeInfo == RoamNodeInfo.end)
			{//终点
				ni.isEnd = true;
				ni.isMain = true;
				ni.endNum = t.childCount;
				string s = ri.nodeName.ToString();
				s = s.Substring(s.IndexOf(".") + 1);
				string s1 = LastNodeName(t.childCount, out ni.startNum);
				ni.showContext = s1 + " 到 " + s;
			}
			else if (ri.nodeInfo == RoamNodeInfo.main)
			{//主节点
				ni.isMain = true;
				ni.endNum = t.childCount;
				string s = ri.nodeName.ToString();
				s = s.Substring(s.IndexOf(".") + 1);
				string s1 = LastNodeName(t.childCount, out ni.startNum);
				ni.showContext = s1 + " 到 " + s;
			}
			else
			{//辅节点
				ni.isAssist = true;
			}
			//实例一个新图片，图片的位置
			GameObject temp = Resources.Load<GameObject>("Prefabs/RoamFlagImg");
			GameObject g = Instantiate(temp);
			g.transform.parent = this.transform.parent;
			g.GetComponent<RectTransform>().position = WorldToUI(t.position);
			//文字内容显示

		}
	}
	void WorldToUI(Vector3 point)
	{
		//根据边界，决定MapBG的图片的大小
		float w = Mathf.Abs(pointRightUp.position.x - pointLeftDown.position.x);
		float h = Mathf.Abs(pointRightUp.position.z - pointLeftDown.position.x);

		if (w >= h)
		{
			this.GetComponent<RectTransform>().sizeDelta = new Vector2();
		}
		else
		{

		}

		//Vector3 = 
		//return 
	}
	string LastNodeName(int index, out int num)
	{
		string s = null;
		int n = 0;
		for (int i = index - 1; i >= 0; i--)
		{
			Transform t = ConfigData.Instance.roamPath[i];
			RoamInfo ri = t.GetComponent<RoamInfo>();
			if (ri.nodeInfo == RoamNodeInfo.main || ri.nodeInfo == RoamNodeInfo.start)
			{
				s = ri.nodeName.ToString();
				n = i;
			}
		}
		num = n;
		s = s.Substring(s.IndexOf(".") + 1);
		return s;
	}
	// Update is called once per frame
	void Update()
	{

	}
}
