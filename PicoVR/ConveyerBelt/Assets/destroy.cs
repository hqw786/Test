using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {
    public void OnCollisionEnter(Collision collision)
    {
        //Destroy(GetComponent<Rigidbody>());
        if (collision.collider.name.Contains("Down"))
        {
            //transform.parent = collision.collider.transform.parent;
            //transform.localScale = Vector3.one;
        }
    }

	// Use this for initialization
	void Start () {
        Invoke("d",15f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void d()
    {
        Destroy(gameObject);
    }
}
