using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeekEffect : MonoBehaviour {

    Vector3 Top;
    Vector3 Mid;
    Vector3 Bottom;

    int curPoint;
    bool isTrans;
	// Use this for initialization
    void Awake()
    {
        Top = new Vector3(83, 16, 0);
        Mid = new Vector3(83, -18, 0);
        Bottom = new Vector3(83, -54, 0);

        curPoint = 0;
    }
	void Start () {
       //this.transform.GetComponent<Text>().text = "hehe";
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.GetComponent<Text>().text = "hehe";
		if(curPoint == 1)
        {
            Destroy(this.gameObject, 1f);
        }
        if (isTrans)
        {
            if (curPoint == 2)
            {
                float y = Mathf.Lerp(transform.localPosition.y, 17f, Time.deltaTime * 10f);
                if (y >= 16)
                {
                    curPoint = 1;
                    transform.localPosition = Top;
                    isTrans = false;
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
                }
            }
            if (curPoint == 3)
            {
                float y = Mathf.Lerp(transform.localPosition.y, -17f, Time.deltaTime * 10f);
                if (y >= -18)
                {
                    curPoint = 2;
                    transform.localPosition = Mid;
                    isTrans = false;
                }
                else
                {
                    transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
                }
            }
        }
	}
    public void setTrans()
    {
        isTrans = true;
    }
    public void setPosition(string s, string con)
    {

        if(s.Equals("Top"))
        {
            transform.localPosition = Top;
            this.GetComponent<Text>().text = con;
            curPoint = 1;
        }

        if(s.Equals("Mid"))
        {
            transform.localPosition = Mid;
            this.GetComponent<Text>().text = con;
            curPoint = 2;
        }
        if(s.Equals("Bottom"))
        {
            transform.localPosition = Bottom;
            this.GetComponent<Text>().text = con;
            curPoint = 3;
        }
    }
}
