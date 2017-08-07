using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonDir : MonoBehaviour {
    Transform person;

	// Use this for initialization
    void Awake()
    {
        person = transform.Find("/Person");
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Quaternion q = Quaternion.identity;
        print(180f - person.eulerAngles.y);
        q = Quaternion.Euler(new Vector3(0f, 0f, 180f - person.eulerAngles.y));
        transform.localRotation = q;
	}
}
