using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandPlayAnimator : MonoBehaviour {
    Animator anim;
    float randTime;
    float timer;
    int randNum;
    bool isIdle;
	// Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        isIdle = true;
    }
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		if(isIdle)
        {
            isIdle = false;
            randNum = Random.Range(2, 7);
            anim.SetInteger("Action", randNum);
            randTime = Random.Range(5f, 10f);
        }
        else
        {
            timer += Time.deltaTime;
            if(timer >= randTime)
            {
                isIdle = true;
                timer = 0f;
            }
            anim.SetInteger("Action", 0);
            //anim.runtimeAnimatorController
        }
	}
}
