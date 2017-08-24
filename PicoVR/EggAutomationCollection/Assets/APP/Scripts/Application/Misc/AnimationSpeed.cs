using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeed : MonoBehaviour {
    [Range(0.01f, 1)]
    public float speed;
    Animation anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
	}
	
	// Update is called once per frame
	void Update () {
        anim["Take 001"].speed = speed;
	}
}
