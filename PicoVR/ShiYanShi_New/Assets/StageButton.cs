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
    StageSelect stageSelect;
    Text text;
    Image progressImage;
    bool isProgress;
    bool isProgressDone;
    float timer = 3f;
    float time;
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
        progressImage = transform.Find("Image").GetComponent<Image>();
        panel = transform.parent.GetComponent<StagePanel>();
        stageSelect = panel.transform.parent.GetComponent<StageSelect>();
        //设置阶段状态
        status = ss;
        //取得阶段数据
        SetStageData(ConfigData.Instance.dicStage[status].GetData());

        time = 0;
        isProgress = false;
        isProgressDone = false;
    }
    public StageInfo GetStageInfo()
    {
        return stage;
    }
    void SetLock()
    {
        time = 0;
        isProgress = false;
        isProgressDone = false;
        progressImage.fillAmount = 0;

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
    void SetNextStageLock()
    {
        time = 0;
        isProgress = false;
        isProgressDone = false;

        if (SYSManager.Instance.curStageStatus != status) return;

        int s = (int)status;
        s++;
        StageState temp = (StageState)s;

        if (s == ConfigData.Instance.Data.Count - 2)
        {
            //DONE:执行到流程结束的事件
            ConfigData.Instance.dicStage[temp].GetData().isLock = false;
            s++;
            temp = (StageState)s;
            //DONE:执行到流程结束的事件
            ConfigData.Instance.dicStage[temp].GetData().isLock = false;
        }
        else if (s < ConfigData.Instance.Data.Count - 2)
        {
            //DONE:执行到流程结束的事件
            ConfigData.Instance.dicStage[temp].GetData().isLock = false;
        }
    }
    public void SetProgressStart()
    {
        if (SYSManager.Instance.curStageStatus != status) return;
        {
            time = 0;
            isProgress = true;
            isProgressDone = false;
        }
    }
	// Use this for initialization
    void Awake()
    {
        
    }
	void Start () 
    {
        //切换流程是执行
        SYSManager.Instance.FlowEndEvent += SetLock;
        //计算整个流程大约需要的时间
        SYSManager.Instance.FlowEnterEvent += SetProgressStart;

        RayUIStage rayStage = transform.parent.parent.GetComponent<RayUIStage>();
        rayStage.SelectedStageEvent += PointerEnterButton;
	}
	
	// Update is called once per frame
	void Update () {
		if(isProgress)
        {
            time += Time.deltaTime;
            progressImage.fillAmount = time / timer;
            if(progressImage.fillAmount >= 1f)
            {
                progressImage.fillAmount = 1f;
            }
            if(time >= timer)
            {
                isProgress = false;
                isProgressDone = true;
            }
        }
        if(isProgressDone)
        {
            isProgressDone = false;
            SetNextStageLock();
        }

        if (SYSManager.Instance.curStageStatus != status)
        {
            if (ConfigData.Instance.dicStage[status].GetData().isLock)
            {
                text.color = Color.gray;
            }
            else
            {
                text.color = Color.white;
            }
        }
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
            OnOperationButtonClick();
        }
    }

    private void OnOperationButtonClick()
    {
        if(!stage.isLock)
        {
            stageSelect.ButtonResetDefault();
            SYSManager.Instance.AppStatusSwitch(AppState.FeedingAndEgg);
            text.color = Color.green;
            SYSManager.Instance.StageStatusSwitch(status);
            SYSManager.Instance.StartShowFlow();
        }
    }

    void OnButtonClick()
    {
        if(!stage.isLock)
        {
            stageSelect.ButtonResetDefault();
            SYSManager.Instance.AppStatusSwitch(AppState.Show);
            text.color = Color.green;
            SYSManager.Instance.StageStatusSwitch(status);
            SYSManager.Instance.StartShowFlow();
        }
    }
    public void scaleButton()
    {
        gameObject.transform.localScale = Vector3.one * 1.15f;
    }
    public void resetButtonToDefault()
    {
        gameObject.transform.localScale = Vector3.one;
        if (SYSManager.Instance.curStagePlayStatus == StagePlayState.none)
        {
            if (ConfigData.Instance.dicStage[status].GetData().isLock)
            {
                text.color = Color.gray;
            }
            else
            {
                text.color = Color.white;
            }
        }
    }
    public void PointerEnterButton(GameObject g)
    {
        if (g == gameObject)
        {
            if (!stage.isLock)
            {
                if (SYSManager.Instance.curStageStatus != status)
                {
                    text.color = Color.red;
                }
            }
        }
    }
}
