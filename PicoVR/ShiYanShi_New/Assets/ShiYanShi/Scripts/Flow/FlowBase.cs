using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class FlowBase
{
    protected bool isFlowEnter;
    protected bool isFlowExec;
    protected bool isFlowExit;
    private StageInfo data;
    public virtual void Enter();
    public virtual void Exec()
    {
        //这是固定的，执行文字显示
        if (ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].data.Context.Count == 0) Debug.LogError("文字内容为空，请查看原因！");

    }
    public virtual void Exit();
    public void SetData(StageInfo si)
    {
        data = si;
    }
    public StageInfo GetData()
    {
        return data;
    }
}

