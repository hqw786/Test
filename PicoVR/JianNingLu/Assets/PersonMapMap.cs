using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonMapMap : MonoBehaviour {
    public MapType mapType; 
    Transform flag;
    Transform pointLeftDown;
    Transform person;
    //GameObject mapBG;
    //Vector3 personPosition;
    Vector2 mapSize;
    Vector2 mapOrigin;
    //Vector2 flagPosition;
    float rate;
	// Use this for initialization
	void Start () {
        person = transform.Find("/Person");
        pointLeftDown = transform.Find("/BoundaryPoints/LeftDown");
        flag = transform.Find("PersonFlag");
        //mapBG = transform.Find("MapBG").gameObject;
        //mapSize = mapBG.GetComponent<RectTransform>().sizeDelta;
        mapSize = GetComponent<RectTransform>().sizeDelta;
        mapOrigin = new Vector2(-mapSize.x * 0.5f, -mapSize.y * 0.5f);
        
        rate = mapType == MapType.normal ? MainManager.Instance.rate : MainManager.Instance.mineRate;
	}
	
	// Update is called once per frame
	void Update () {
        flag.localPosition = WorldToUI(person.position);
	}
    Vector3 WorldToUI(Vector3 point)
    {
        //计算距左下角点的距离
        float x = Mathf.Abs(point.x - pointLeftDown.position.x) * rate;
        float y = Mathf.Abs(point.z - pointLeftDown.position.z) * rate;

        Vector3 pos = new Vector3(mapOrigin.x + x, mapOrigin.y + y, 0);
        return pos;
    }
}
