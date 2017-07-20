using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamPoints : MonoBehaviour 
{
	public Transform pointLeftDown;
	public Transform pointRightUp;
	public Transform rootNode;
    [HideInInspector]
	public GameObject mapBG;
    [HideInInspector]
    public GameObject mapBG1;
    [HideInInspector]
    public GameObject mapBG2;

    WarpPoints warpPoints;

	Vector2 mapSize;
	Vector2 mapOrigin;
	//Vector2 flagPosition;
    [HideInInspector]
    public float rate;
    [HideInInspector]
    public float mineRate;
	// Use this for initialization
	void Awake()
	{
		//取得UI
        mapBG = rootNode.Find("MapBG").gameObject;
        mapBG1 = rootNode.parent.Find("NewPositionPanel").Find("MapBG").gameObject;
        mapBG2 = rootNode.parent.Find("MineMapPanel").Find("MapBG").gameObject;
        warpPoints = transform.Find("/WarpPoints").GetComponent<WarpPoints>();
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
        //设置MapBG的大小
        float w1, h1;
        w1 = 1024f;
        rate = w1 / mapSize.x;//把比率保存下来
        h1 = rate * mapSize.y;
        Vector2 temp = new Vector2(w1, h1);
        mapBG.GetComponent<RectTransform>().sizeDelta = temp;
        mapBG1.GetComponent<RectTransform>().sizeDelta = temp;
        //设置小地图MapBG的大小 
        w1 = 320f;
        mineRate = w1 / mapSize.x;
        h1 = mineRate * mapSize.y;
        Vector2 tempMine = new Vector2(w1, h1);
        mapBG2.GetComponent<RectTransform>().sizeDelta = tempMine;
        //设置MapBG的原点为左下角点
        mapOrigin = new Vector2(-temp.x * 0.5f, -temp.y * 0.5f);
        //把比率保存起来
        MainManager.Instance.rate = rate;
        MainManager.Instance.mineRate = mineRate;
	}
	void Start()
	{
        //MapBG大小
        SetMap();

        //映射传送点到地图
        warpPoints.pointLeftDown = pointLeftDown;
        warpPoints.mapBG = mapBG1;
        warpPoints.mapOrigin = mapOrigin;
        warpPoints.rate = rate;
        warpPoints.MapWarpToMap();

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
                    //= Tools.WorldToUI(ni.transform.position, pointLeftDown.position, mapOrigin, MainManager.Instance.rate);
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
