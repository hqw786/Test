using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlowBase
{
    public event Action enterEvent;
    public event Action execEvent;
    public event Action exitEvent;
    public event Action defaultEvent;
    
    public List<Transform> enterTransform = new List<Transform>();
    public List<Transform> execTransform = new List<Transform>();
    public List<Transform> exitTransform = new List<Transform>();

    protected StageInfo data;
    public virtual void Enter()
    {
        //一定有的，模型慢慢显示出来
        Debug.Log("模型渐渐显示");
        StageCoroutineManager.Instance.StageModelShow();
    }
    public virtual void Exec()
    {
        //一定有的，执行文字显示
        if (ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].data.Context.Count == 0)
            Debug.LogError("文字内容为空，请查看原因！");
        StageCoroutineManager.Instance.StageContentDisplay();
    }
    public virtual void Exit()
    {
        //一定有的，模型慢慢隐藏起来
        Debug.Log("模型渐渐隐藏");
        StageCoroutineManager.Instance.StageModelHide();
    }
    public void SetData(StageInfo si)
    {
        data = si;
    }
    public StageInfo GetData()
    {
        return data;
    }
    public IEnumerator ModelTransitionShow()
    {//模型渐渐显示，向模型发送显示消息。
        data.MainModel.SetActive(true);
        data.MainModel.transform.parent = SYSManager.Instance.modelPoint.transform;
        data.MainModel.transform.localPosition = Vector3.zero;

        data.MainModel.BroadcastMessage("SetObject", SendMessageOptions.DontRequireReceiver);
        data.MainModel.BroadcastMessage("SetShow", SendMessageOptions.DontRequireReceiver);

        yield return new WaitForSeconds(1.5f);
        SYSManager.Instance.isFlowEnterDone = true;
    }
    public IEnumerator ModelTransitionHide()
    {//模型渐渐隐藏，向模型发送隐藏消息。
        data.MainModel.BroadcastMessage("SetHide", SendMessageOptions.DontRequireReceiver);
        yield return new WaitForSeconds(1.5f);
        SYSManager.Instance.isFlowEnd = true;
    }
    public virtual IEnumerator EnterCoroutine()
    {
        yield return null;
    }
    public virtual IEnumerator ExitCoroutine()
    {
        yield return null;
    }

    public virtual void Default()
    {
        if(defaultEvent != null)
        {
            defaultEvent();
        }
    }
    public virtual void SetDefault()
    {
        SYSManager.Instance.FlowSwitchEvent += Default;
    }
}

