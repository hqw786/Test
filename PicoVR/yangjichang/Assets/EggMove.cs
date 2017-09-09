using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggMove : MonoBehaviour {
    public float speed;
    public float force;
    bool isToConveror;
    bool isFreeMove;
    Rigidbody rig;

    public void OnTriggerEnter(Collider other)
    {
        if (!isFreeMove)
        {
            if (other.name.Contains("RigibodyBlock"))
            {
                isFreeMove = true;
                rig.useGravity = true;
                rig.isKinematic = false;
                rig.AddForce(-Vector3.right * force);
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(!isToConveror)
        {
            if(collision.collider.name.Contains("ConveyorBox"))
            {
                Invoke("CancelGravity", 1f);
            }
        }
        if(collision.collider.name.Contains("DestroyBlock"))
        {
            Destroy(gameObject);
        }
    }
    void CancelGravity()
    {
        isToConveror = true;
        rig.useGravity = false;
        rig.isKinematic = false;
    }
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isFreeMove)
        {
            if (isToConveror)
            {
                transform.Translate( -Vector3.right * Time.deltaTime * speed, Space.World);
            }
        }
	}
}
