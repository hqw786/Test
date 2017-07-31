using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageButton : MonoBehaviour ,IPointerClickHandler
{
    StageInfo stage;
    StageState status;
    StagePanel panel;
    Text text;

    public void SetStageData(StageInfo si)
    {
        stage = si;
        if(si.isLock)
        {
            text.color = Color.gray;
            //TODO:这里修改成要改变以效果
        }
        else
        {
            text.color = Color.white;
        }
        text.text = stage.Name;
    }
    public void SetStageState(StageState ss)
    {
        text = transform.Find("Text").GetComponent<Text>();
        panel = transform.parent.GetComponent<StagePanel>();
        //设置阶段状态
        status = ss;
        //取得阶段数据
        SetStageData(ConfigData.Instance.dicStage[status].GetData());
    }
    public StageInfo GetStageInfo()
    {
        return stage;
    }
    void SetLock()
    {
        if (SYSManager.Instance.curStageStatus != status) return;
        ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].GetData().isLock = false;
        if (ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].GetData().isLock)
        {
            text.color = Color.gray;
        }
        else
        {
            text.color = Color.white;
        }
    }
	// Use this for initialization
    void Awake()
    {
        
    }
	void Start () 
    {
        SYSManager.Instance.FlowEndEvent += SetLock;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        print("你按了阶段按钮： " + eventData.pointerEnter.name);
        //按下了阶段按钮，取得对象
        //int sn = (int)status;
        if (status <= StageState.chandq)
        {
            OnButtonClick();
        }
        else
        {
            OnShowButtonClick();
        }
    }

    private void OnShowButtonClick()
    {
        if(!stage.isLock)
        {
            SYSManager.Instance.AppStatusSwitch(AppState.FeedingAndEgg);
            panel.ButtonResetDefault();
            text.color = Color.red;
            SYSManager.Instance.StageStatusSwitch(status);
            SYSManager.Instance.StartShowFlow();
        }
    }

    void OnButtonClick()
    {
        if(!stage.isLock)
        {
            SYSManager.Instance.AppStatusSwitch(AppState.Show);
            panel.ButtonResetDefault();
            text.color = Color.red;
            SYSManager.Instance.StageStatusSwitch(status);
            SYSManager.Instance.StartShowFlow();
        }
    }
}
