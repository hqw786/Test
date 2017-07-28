using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageCoroutineManager : MonoBehaviour {
    public static StageCoroutineManager Instance;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
    }
    //void Start () {
		
    //}
	
    //// Update is called once per frame
    //void Update () {
		
    //}

    public void StageFuHQEnter()//阶段孵化期进入流程
    {
        StartCoroutine(ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].EnterCoroutine());
    }
    public void StageFuHQExit()
    {
        StartCoroutine(ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].ExitCoroutine());
    }
    public void StageContentDisplay()
    {
        StartCoroutine(Tools.DisplayContent(ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].GetData().Context, SYSManager.CONTENTTIME, SYSManager.CONTENTALPHATIME));
    }
    public void StageModelShow()
    {
        StartCoroutine(ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].ModelTransitionShow());
    }
    public void StageModelHide()
    {
        StartCoroutine(ConfigData.Instance.dicStage[SYSManager.Instance.curStageStatus].ModelTransitionHide());
    }
}
