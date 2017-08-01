using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuHuaXiang : MonoBehaviour {
    Animation anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        //anim.Play("fuhuaxiang");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void waitState()
    {
        anim["fuhuaxiang_001"].speed = 0;
        BottomRotation();
    }
    public void continuePlay()
    {
        anim["fuhuaxiang_001"].speed = 1;
        //Invoke("BottomRotation", 3.5f);
    }
    void BottomRotation()
    {
        BroadcastMessage("StartRotation");
    }
    public  void ResetBox()
    {
        anim["fuhuaxiang_001"].speed = 10;
    }
}
