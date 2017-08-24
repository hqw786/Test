using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour 
{
    Rigidbody rig;
    
    bool isFixed;
    bool canMove;
    bool isCheckCollision;

    public float force;
    public float speedForce;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name.Contains("Plane"))
        {
            isFixed = true;
        }
        else
        {
            isFixed = false;
        }
    }



	// Use this for initialization
	void Start () {
		//刚生成就给个力
        rig = GetComponent<Rigidbody>();
        rig.AddForce(Vector3.forward * force);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.position.y  <= 0)
        {
            Destroy(gameObject);
        }
	}
    void FixedUpdate()
    {
        if(isFixed)
        {
            rig.AddForce(-Vector3.right * speedForce);
        }
    }
}
