using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipAnimationSpeed : MonoBehaviour {
    Animation anim;

    [Range(0, 1)]
    public float speed;
	// Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animation>();
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        anim["Take 001"].speed = speed;
	}
}
