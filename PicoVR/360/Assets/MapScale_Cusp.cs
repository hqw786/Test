using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapScale_Cusp : MonoBehaviour {
    Scrollbar scroll;
    GameObject left;
    GameObject right;
	// Use this for initialization
    void Awake()
    {
        scroll = GetComponent<Scrollbar>();
        left = transform.Find("Left").gameObject;
        right = transform.Find("Right").gameObject;
    }
	void Start () {
        OnScrollBarValueChange();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnScrollBarValueChange()
    {
        if(scroll.value <= 0.05f)
        {
            left.SetActive(false);
        }
        else if(scroll.value >= 0.95f)
        {
            right.SetActive(false);
        }
        else
        {
            left.SetActive(true);
            right.SetActive(true);
        }
    }
}
