using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpPoints : MonoBehaviour {
    [HideInInspector]
    public Transform pointLeftDown;
    [HideInInspector]
    public GameObject mapBG;
    [HideInInspector]
    public float rate;
    [HideInInspector]
    public Vector2 mapOrigin;

    WarpInfo warpInfo;
    // Use this for initialization
    void Awake()
    {
        
    }
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    Vector3 WorldToUI(Vector3 point)
    {
        //计算距左下角点的距离
        float x = Mathf.Abs(point.x - pointLeftDown.position.x) * rate;
        float y = Mathf.Abs(point.z - pointLeftDown.position.z) * rate;

        Vector3 pos = new Vector3(mapOrigin.x + x, mapOrigin.y + y, 0);
        return pos;
    }
    public void MapWarpToMap()
    {
        foreach(Transform t in transform)
        {
            warpInfo = t.GetComponent<WarpInfo>();
            GameObject gt = Resources.Load<GameObject>("Prefabs/BtnPoint");
            GameObject g = Instantiate(gt);
            g.transform.parent = mapBG.transform.parent;
            g.transform.localScale = Vector3.one;
            g.GetComponent<RectTransform>().localPosition = WorldToUI(t.position);
            ShowText st = g.GetComponent<ShowText>();
            st.SetContext(warpInfo.warpName.ToString());
            st.point = t;
            g.name = t.name;
        }
    }
}
