using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpManager : MonoBehaviour {
    private List<HelpArgs> args = new List<HelpArgs>();
    public static HelpManager Instance;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
        HelpArgs[] a= FindObjectsOfType<HelpArgs>();
        args.AddRange(a);
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ExecuteHelpAction(HelpArgs ha)
    {
        print(ha.context);
        //跟UI交互
        switch(ha.actionMode)
        {
            case HelpActionMode.TopHint:
                {
                    Facade.Instance.HandleMessage(Consts.Msg_UI_Help_TopHint, ha);
                }
                break;
            case HelpActionMode.ToolTip:
                {
                     Facade.Instance.HandleMessage(Consts.Msg_UI_Help_ToolTip, ha);
                }
                break;
            case HelpActionMode.ToolTipToTask:
                {

                }
                break;
        }
    }
}
