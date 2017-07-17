using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamPoints : MonoBehaviour 
{
	public Transform pointLeftDown;
	public Transform pointRightUp;
	public Transform rootNode;

	public GameObject mapBG;


	Vector2 mapSize;
	Vector2 mapOrigin;
	Vector2 flagPosition;
    float rate;
	// Use this for initialization
	void Awake()
	{
		//取得UI
        mapBG = rootNode.Find("MapBG").gameObject;
	}
	void SetMap()
	{
		//根据边界，决定MapBG的图片的大小
		float w = Mathf.Abs(pointRightUp.position.x - pointLeftDown.position.x);
		float h = Mathf.Abs(pointRightUp.position.z - pointLeftDown.position.z);
		if (w >= h)
		{
			mapSize = new Vector2(w, h);
		}
		else
		{
			mapSize = new Vector2(h, w);
		}
        float w1, h1;
        w1 = 1024f;
        rate = w1 / mapSize.x;//把比率保存下来
        h1 = rate * mapSize.y;
        mapSize = new Vector2(w1, h1);
        //设置MapBG的大小
        mapBG.GetComponent<RectTransform>().sizeDelta = mapSize;
        //设置MapBG的原点为左下角点
        mapOrigin = new Vector2(-mapSize.x * 0.5f, -mapSize.y * 0.5f);
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
        PathNodeMapToMap();
	}
	void GetPathNodeInfo()
	{
		//foreach (Transform t in ConfigData.Instance.roamPath)
        for (int i = 0; i < ConfigData.Instance.roamPath.Count;i++)
        {
            NodeInfo ni = new NodeInfo();
            RoamInfo ri = ConfigData.Instance.roamPath[i].GetComponent<RoamInfo>();
            ni.transform = ConfigData.Instance.roamPath[i];
            if (ri.nodeInfo == RoamNodeInfo.start)
            {//起点
                ni.isStart = true;
                ni.isMain = true;
                ni.startNum = -1;
                ni.endNum = 0;
                string s = ri.nodeName.ToString();
                s = s.Substring(s.IndexOf(".") + 1);
                ni.showContext = "起点：" + s;
            }
            else if (ri.nodeInfo == RoamNodeInfo.end)
            {//终点
                ni.isEnd = true;
                ni.isMain = true;
                ni.endNum = i;
                string s = ri.nodeName.ToString();
                s = s.Substring(s.IndexOf(".") + 1);
                string s1 = LastNodeName(i, out ni.startNum);
                ni.showContext = s1 + " 到 终点：" + s;
            }
            else if (ri.nodeInfo == RoamNodeInfo.main)
            {//主节点
                ni.isMain = true;
                ni.endNum = i;
                string s = ri.nodeName.ToString();
                s = s.Substring(s.IndexOf(".") + 1);
                string s1 = LastNodeName(i, out ni.startNum);
                ni.showContext = s1 + " 到 " + s;
            }
            else
            {//辅节点
                ni.isAssist = true;
            }
            ConfigData.Instance.pathNodeInfo.Add(ni);
        }
	}
	Vector3 WorldToUI(Vector3 point)
	{
		//计算距左下角点的距离
        float x = Mathf.Abs(point.x - pointLeftDown.position.x) * rate;
        float y = Mathf.Abs(point.z - pointLeftDown.position.z) * rate;

        Vector3 pos = new Vector3(mapOrigin.x + x, mapOrigin.y + y, 0);
        return pos;
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
                break;
			}
		}
		num = n;
		s = s.Substring(s.IndexOf(".") + 1);
		return s;
	}
    void PathNodeMapToMap()
    {
        foreach (NodeInfo ni in ConfigData.Instance.pathNodeInfo)
        {
            if (!ni.isAssist)
            {

                //实例一个新图片，图片的位置
                GameObject temp = Resources.Load<GameObject>("Prefabs/BtnRoamFlagImg");
                GameObject g = Instantiate(temp);
                g.transform.parent = rootNode;
                g.transform.localScale = Vector3.one;
                g.GetComponent<RectTransform>().localPosition = WorldToUI(ni.transform.position);
                //文字内容显示
				UpdateContext(g, ni);
            }
        }
    }
    void UpdateContext(GameObject g,NodeInfo ni)
    {
        g.GetComponent<ShowPathInfo>().SetPathNodeInfo(ni);
        //如果脚本不执行，再想办法
    }
	// Update is called once per frame
	void Update()
	{

	}
}
