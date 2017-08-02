using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFuHQ : FlowBase
{
    public override void Enter()
    {
        StageCoroutineManager.Instance.StageFuHQEnter();
    }
    public override void Exit()
    {
        StageCoroutineManager.Instance.StageFuHQExit();
    }
    public override IEnumerator EnterCoroutine()
    {
        Debug.Log("孵化箱上升");
        enterTransform[0].parent.GetComponent<Animation>().CrossFade("fuhuaxiang_001");
        //电视开机(这边以后可以做成渐渐开机效果，显示LOGO）
        Debug.Log("屏幕打开");
        enterTransform[1].parent.GetComponent<PingMuTrans>().SetDisplay();
        yield return new WaitForSeconds(5f);
        base.Enter();
    }
    public override IEnumerator ExitCoroutine()
    {
        //箱子降下去
        enterTransform[0].parent.GetComponent<FuHuaXiang>().continuePlay();
        yield return new WaitForSeconds(3f);
        base.Exit();
    }
    //public override void SetDefault()
    //{
    //    SYSManager.Instance.FlowSwitchEvent += Default;
    //}
}
