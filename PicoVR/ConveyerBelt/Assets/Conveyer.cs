using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyer : MonoBehaviour {
    Vector3 leftPosition = new Vector3(0, 0, -7.5f);
    Vector3 rightPosition = new Vector3(0, 0, 8.5f);
    Transform temp;

    Queue<Transform> box = new Queue<Transform>();
	// Use this for initialization
	void Start () {
		foreach(Transform t in transform)
        {
            box.Enqueue(t);
        }
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        temp = box.Peek();
        if (temp.position.z >= 8.5f)
        {
            temp = box.Dequeue();
            temp.position = leftPosition;
            box.Enqueue(temp);
        }

        transform.Translate(transform.forward * Time.fixedDeltaTime * 0.2f);
    }


	void Update () 
    {
        
	}
    //最左边位置
    //
}
