using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour {
    public void OnCollisionEnter(Collision collision)
    {
        Destroy(GetComponent<Rigidbody>());
        transform.parent = collision.collider.transform.parent;
        transform.localScale = Vector3.one;
    }

	// Use this for initialization
	void Start () {
        Invoke("d",3f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void d()
    {
        Destroy(gameObject);
    }
}
