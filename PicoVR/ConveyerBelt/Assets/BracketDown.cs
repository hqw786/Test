using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BracketDown : MonoBehaviour {

    Vector3 downPosition;
    float up = -2.42f;
    Transform temp;

    Queue<Transform> box = new Queue<Transform>();
    // Use this for initialization
    void Start()
    {
        foreach (Transform t in transform)
        {
            box.Enqueue(t);
        }
        Transform tt = box.Peek();
        downPosition = new Vector3(tt.position.x, 2.11f, tt.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
 temp = box.Peek();
        if (temp.position.y <= up)
        {
            temp = box.Dequeue();
            temp.position = downPosition;
            box.Enqueue(temp);
        }

        transform.Translate(-transform.up * Time.fixedDeltaTime * 0.2f);
    }
    void Update()
    {
       
    }
}
