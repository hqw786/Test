using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : MonoBehaviour {//到时可能要拆分成三个类

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnShareCloseClick()
    {
        if(this.name.Contains("Share"))
        {
            gameObject.SetActive(false);
        }
    }
    public void OnAboutCloseClick()
    {
        if (this.name.Contains("About"))
        {
            gameObject.SetActive(false);
        }
    }
    public void OnCommentCloseClick()
    {
        if (this.name.Contains("Comment"))
        {
            gameObject.SetActive(false);
        }
    }
}
