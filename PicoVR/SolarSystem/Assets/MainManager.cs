using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour {
    public static MainManager Instance;
    Camera camera;
    Transform xingQiu; 
    List<Animation> animList = new List<Animation>();
    public event System.Action<float> AnimationAddSpeedEvent;
    public event System.Action AnimationNormalSpeedEvent;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
        camera = Camera.main;
        //camera.enabled = false;
        xingQiu = transform.Find("/xingqiu");
    }
	void Start () {

        if(AnimationAddSpeedEvent != null)
        {
            float rand = Random.Range(20, 50);
            AnimationAddSpeedEvent(rand);
        }
        Invoke("Display", 3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Display()
    {
        if(AnimationNormalSpeedEvent != null)
        {
            AnimationNormalSpeedEvent();
        }
        //camera.enabled = true;
    }
}
