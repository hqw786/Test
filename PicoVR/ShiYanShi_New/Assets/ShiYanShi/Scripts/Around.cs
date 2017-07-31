using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Around : MonoBehaviour {
    bool isStartRotation;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (SYSManager.Instance.curAppStatus == AppState.Show)
        {
            if (isStartRotation)
            {
                transform.Rotate(transform.up, 1f, Space.Self);
            }
        }
        if(SYSManager.Instance.curAppStatus == AppState.FeedingAndEgg)
        {
            if(this.name.Contains("jidan"))
            {
                transform.Rotate(transform.up, 1f, Space.Self);
            }
        }
	}
    void StartRotation()
    {
        isStartRotation = true;
    }
}
