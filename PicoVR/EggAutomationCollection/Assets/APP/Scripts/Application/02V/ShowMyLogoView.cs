using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMyLogoView : View {
    public override string Name
    {
        get { return Consts.V_ShowMyLogo; }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void HandleEvent(string eventName, object data)
    {
        switch(eventName)
        {
            case Consts.C_ShowMyLogo:

                break;
        }
    }
}
