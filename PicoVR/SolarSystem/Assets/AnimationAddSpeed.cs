using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationAddSpeed : MonoBehaviour {
    public string animName;
    Animation anim;
	// Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animation>();
    }
	void Start () {
        RegisterEvent();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void AddSpeed(float speed)
    {
        anim[animName].speed = speed;
    }
    void NormalSpeed()
    {
        anim[animName].speed = 1f;
    }
    void RegisterEvent()
    {
        MainManager.Instance.AnimationAddSpeedEvent += AddSpeed;
        MainManager.Instance.AnimationNormalSpeedEvent += NormalSpeed;
    }
}
