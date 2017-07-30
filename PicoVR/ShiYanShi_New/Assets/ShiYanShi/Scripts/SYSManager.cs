using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Pvr_UnitySDKAPI;

public enum StageState
{
    fuhq,
    pokq,
    miaojq,
    chujq,
    qingnq,
    chengnq,
    chandq,
    weisq,
    egg
}
public enum AppState
{
    Init,
    Show,
    Feeding,
    EggShow,
    End
}
public enum PlayState
{
    auto,
    once
}
public class SYSManager : MonoBehaviour 
{
    public static SYSManager Instance;
    public const float FADETIME = 1.5f;
    public const float CONTENTTIME = 3f;
    public const float CONTENTALPHATIME = 1f;
    public Dictionary<AppState, List<GameObject>> dicEgg = new Dictionary<AppState, List<GameObject>>();
    public Dictionary<string, string[]> dicEggContent = new Dictionary<string, string[]>();

    public event System.Action FlowEndEvent;

    public AppState curAppStatus;
    public StageState curStageStatus;
    public PlayState playStatus;

    public GameObject gazePointer;
    public GameObject leftEye;
    public GameObject rightEye;
    public GameObject endMenu;
    public GameObject selectMenu;
    public Transform fuhuaxiang;
    public GameObject modelPoint;
    public GameObject content;
    GameObject tempObject;
    public Transform weekContent;
    public GameObject eggLaying;

    public bool isFadeIn;
    public bool isFadeOut;
    public bool isLastModelAlpha;//可以将上一个模型变透明
    public bool isCurModelAlpha;//可以将当前状态模型显示
    public bool isTempModelAlpha;//临时模型显示
    public bool isModelAlphaDone;
    public bool isContentAlphaHide;
    public bool isContentAlphaDisplay;

	public bool isFlowEnterDone;
	public bool isFlowExecDone;
    public bool isFlowStart;//一个新阶段流程开始
    public bool isFlowEnd;


    bool isMenuTips;
    bool isEggTips;

    Color curModelColor;
    Color lastModelColor;
    Material curModelMaterial;
    Material lastModelMaterial;
    GameObject lastGameObject;
    GameObject curGameObject;

    BoxCollider bcBook;

    Text contentTemp;
    Text contentHumi;
    Text contentOther;
    Text contentDes;

    public Shader shaderN;
    public Shader shaderT;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
        curAppStatus = AppState.Init;
        playStatus = PlayState.once;
        endMenu.SetActive(false);

        shaderN = Shader.Find("Mobile/Diffuse");
        shaderT = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        //contentWeek = content.transform.parent.Find("Week").GetComponent<Text>();
        contentTemp = content.transform.Find("Temp").GetComponent<Text>();
        contentHumi = content.transform.Find("Humi").GetComponent<Text>();
        contentOther = content.transform.Find("Other").GetComponent<Text>();
        contentDes = content.transform.Find("Des").GetComponent<Text>();

        //鸡蛋模型
        List<GameObject> egg = new List<GameObject>();
        GameObject g = Resources.Load<GameObject>("ji/jidan_02");
        egg.Add(Instantiate(g));
        g = Resources.Load<GameObject>("ji/jidan_03");
        egg.Add(Instantiate(g));
        g = Resources.Load<GameObject>("ji/jidan_01");
        egg.Add(Instantiate(g));
        dicEgg.Add(AppState.EggShow, egg);
        dicEgg[AppState.EggShow][0].SetActive(false);
        dicEgg[AppState.EggShow][1].SetActive(false);
        dicEgg[AppState.EggShow][2].SetActive(false);

        
    }
	void Start () {
		//鸡蛋数据
		print(ConfigData.Instance.Data.Count);
        dicEggContent.Add("jidan_02", ConfigData.Instance.Data[ConfigData.Instance.Data.Count - 1].Context[0]);
        dicEggContent.Add("jidan_03", ConfigData.Instance.Data[ConfigData.Instance.Data.Count - 1].Context[1]);
        dicEggContent.Add("jidan_01", ConfigData.Instance.Data[ConfigData.Instance.Data.Count - 1].Context[2]);
        
		//第一次将摄像头转到90度。以后不用旋转
        if (Manager.isFirst)
        {
            Manager.isFirst = false;
            leftEye.transform.parent.parent.rotation = Quaternion.identity;
            leftEye.transform.parent.parent.eulerAngles = new Vector3(0, -90, 0);
        }
        //如果遥控器连接则隐藏凝视（没有效果）
        if (Controller.UPvr_GetControllerState() == ControllerState.Connected)
        {
            gazePointer.SetActive(false);
        }
        //第一次显示提示
        if(Manager.isMenuTips)
        {
            Manager.isMenuTips = false;
            transform.Find("Menu/Tips").gameObject.SetActive(true);
        }
        else
        {
            transform.Find("Menu/Tips").gameObject.SetActive(false);
        }
	}
	public void OnBtnReturnClick()
    {
        //vr回到原位 
        leftEye.transform.parent.GetComponent<CameraScale>().ReturnOriginPosition();

        //重置传感器（以当前方向为原始位置）。
        if (Pvr_UnitySDKManager.pvr_UnitySDKSensor != null)
        {
            Pvr_UnitySDKManager.pvr_UnitySDKSensor.ResetUnitySDKSensor();
        }

        selectMenu.SetActive(true);
        endMenu.SetActive(false);
        SceneManager.LoadScene("Shiyanshi");
    }
    public void OnBtnExitClick()
    {
        print("退出");
        Application.Quit();
    }
    public void OnBtnRestartClick()
    {
        ResetState();
    }
    public void StartShowFlow(StageState ss)
    {
        isFlowStart = true;
        curAppStatus = AppState.Show;
        curStageStatus = ss;
    }
    public void StartShowFlow()
    {
        isFlowStart = true;
    }
	void Update ()
    {
        #region 新的主流程
        if (curAppStatus == AppState.Show)
        {
            if (isFlowStart)
            {
                isFlowStart = false;
                //进入当前阶段要做的事（前奏，准备工作，一般每个阶段不同）
                //翻页周数
                TransWeek("Bottom", ConfigData.Instance.dicStage[curStageStatus].GetData().Context[0][0]);
                Debug.Log("SYSManager的流程开始，进入Enter");
                ConfigData.Instance.dicStage[curStageStatus].Enter();
            }
            if(isFlowEnterDone)
            {
                isFlowEnterDone = false;
                //当前阶段要做的事（一般是相同的事）
                Debug.Log("SYSManager的流程准备完成。。。进入Exec");
                ConfigData.Instance.dicStage[curStageStatus].Exec();
            }
            if(isFlowExecDone)
            {
                isFlowExecDone = false;
                //退出当前阶段前要做的事（结束工作，一般每个阶段不同）
                Debug.Log("SYSManager的流程执行结束，进入Exit");
                ConfigData.Instance.dicStage[curStageStatus].Exit();
            }
            if(isFlowEnd)
            {
                isFlowEnd = false;
                //切换到下一个状态（自动，手动。手动就恢复默认值状态）
                Debug.Log("SYSManager的流程结束，切换阶段");
                
                StageStatusSwitch();
                StageStatusReset();
            }
        }
        if(curAppStatus == AppState.EggShow || curAppStatus == AppState.Feeding)
        {

        }
        #endregion
        #region 原来的主流程
        ////if(!isFirst &&　Time.time > 5f)
        ////{
        ////    isFirst = true;
        ////    ClickBook(book);
        ////}
        ////到了最后
        ////if (curState == SYSState.END)
        ////{
        ////    //再看一遍显示出来
        ////    endMenu.transform.Find("Restart").gameObject.SetActive(true);
        ////    contentDes.gameObject.SetActive(true);
        ////    isContentAlphaDisplay = true;
        ////    contentDes.text = "谢谢观看！";
        ////    return;
        ////}
        ////到了产蛋阶段
        //if(curState == SYSState.EggLaying || curState == SYSState.EggShow)
        //{
        //    isStateStart = true;//这个设置为真就不会进么状态循环
        //    if(curState == SYSState.EggLaying)
        //        eggLayingShow();
        //}
        ////进入状态前
        //if (!isStateStart)
        //{
        //    print("进入" + curState + "状态");
        //    isStateStart = true;
        //    isStateEnterDone = false;
        //    isStateExecDone = false;
        //    isStateExitDone = false;
        //    //协和执行,完成后isStateEnterDone为True
        //    StartCoroutine(EnterState());
        //}

        ////执行状态中
        //if (isStateEnterDone)
        //{
        //    print(curState + "状态");
        //    isStateEnterDone = false;
        //    //协和执行，完成后isStateExecDone为True
        //    if(curState != SYSState.Select)
        //        StartCoroutine(ExecState());
        //}

        ////退出状态
        //if (isStateExecDone)
        //{
        //    print("退出" + curState + "状态");
        //    isStateExecDone = false;
			
        //    //切换状态
        //    print("切换到下一个状态");
        //    comeNextState();

        //    //协和执行，完成后isStateExitDone为True
        //    StartCoroutine(ExitState());
        //}

        ////切换状态
        //if (isStateExitDone)
        //{
        //    isStateEnterDone = false;
        //    isStateStart = false;
        //    print("可以进入下一个状态");
        //}
        ////模型切换效果
        //TransGameObject();
		#region 调试完成就删除

		#endregion
        #endregion
    }

    //产蛋展示
    void eggLayingShow()
    {
        //过程：显示UI，点击显示蛋分类，点击不同的蛋显示相应的介绍
        //周数隐藏，
        weekContent.gameObject.SetActive(false);
        //显示产蛋UI
        eggLaying.transform.Find("eggLaying").gameObject.SetActive(true);
        //dicGameObject[SYSState.ChanDJ].GetComponent<MeshRenderer>().material.shader = shader1;
        eggLaying.transform.Find("eggLaying").BroadcastMessage("SetShadingDisplay", SendMessageOptions.DontRequireReceiver);
        if(!Manager.isEggTips)
        {
            Manager.isEggTips = true;
            eggLaying.transform.Find("Tips").gameObject.SetActive(true);
        }
    }
    public void OnBtnEggLayingClick()
    {
        //产蛋按钮被点击
        //产蛋Panel渐渐隐藏,停止转动展示转盘
        //dicGameObject[SYSState.ChanDJ].GetComponent<MeshRenderer>().material.shader = shader1;
        //eggLaying.transform.Find("eggLaying").BroadcastMessage("SetShadingHide", SendMessageOptions.DontRequireReceiver);
        //curState = SYSState.EggShow;
        //eggLaying.transform.Find("Tips").gameObject.SetActive(false);
        //modelPoint.transform.parent.GetComponent<Around>().enabled = false;
        ////蛋种类Panel渐渐显示,鸡模型隐藏，三种蛋显示出来
        ////eggLaying.transform.Find("Egg").gameObject.SetActive(true);
        ////eggLaying.transform.Find("Egg").BroadcastMessage("SetShadingDisplay",SendMessageOptions.DontRequireReceiver);
        //dicGameObject[SYSState.ChanDJ].SetActive(false);
        ////
        //weekContent.gameObject.SetActive(true);
        //foreach(Transform t in weekContent)
        //{
        //    t.gameObject.SetActive(false);
        //}
        //GameObject gg = Instantiate(Resources.Load<GameObject>("Week"));
        //gg.transform.parent = weekContent;
        //gg.transform.localScale = weekContent.localScale;
        //gg.GetComponent<WeekEffect>().setPosition("Mid", "选择鸡蛋了解产蛋信息");

        //int index = -1;
        //modelPoint.transform.parent.rotation = Quaternion.identity;
        //foreach(GameObject g in dicEgg[SYSState.EggShow])
        //{
        //    g.SetActive(true);
        //    g.transform.parent = modelPoint.transform;
        //    g.transform.rotation = Quaternion.identity;
        //    g.transform.localPosition = Vector3.zero + new Vector3(0.15f * index, 0f, 0f);
        //    g.transform.localScale = modelPoint.transform.localScale;
        //    g.AddComponent<BoxCollider>();
        //    index++;
        //}
    }
    void tipsHide()
    {
        if(weekContent.gameObject.activeInHierarchy)
        {
            weekContent.gameObject.SetActive(false);
        }
    }
    public void OnBtnRedClick()
    {
        tipsHide();
        content.BroadcastMessage("SetShadingHide", SendMessageOptions.DontRequireReceiver);
        //content.BroadcastMessage("SetTextColor",Color.black,SendMessageOptions.DontRequireReceiver);
        Invoke("RedEggShading", 1f);
    }
    void RedEggShading()
    {
        contentDisplay(dicEggContent["jidan_02"]);
        content.BroadcastMessage("SetShadingDisplay", SendMessageOptions.DontRequireReceiver);
    }
    public void OnBtnGreenClick()
    {
        tipsHide();
        HideContent();
        //content.BroadcastMessage("SetTextColor", Color.black, SendMessageOptions.DontRequireReceiver);
        Invoke("GreenEggShading", 1f);
    }
    void GreenEggShading()
    {
        contentDisplay(dicEggContent["jidan_03"]);
        content.BroadcastMessage("SetShadingDisplay", SendMessageOptions.DontRequireReceiver);
    }
    public void OnBtnPinkClick()
    {
        tipsHide();
        HideContent();
        //content.BroadcastMessage("SetTextColor", Color.black, SendMessageOptions.DontRequireReceiver);
        Invoke("PinkEggShading", 1f);
    }
    void PinkEggShading()
    {
        contentDisplay(dicEggContent["jidan_01"]);
        content.BroadcastMessage("SetShadingDisplay", SendMessageOptions.DontRequireReceiver);
    }
    void changeCurModel(GameObject g)
    {
        g.SetActive(true);
        curGameObject = g;
        g.transform.parent = modelPoint.transform;
        g.transform.localPosition = Vector3.zero;
        curModelMaterial = g.GetComponent<MeshRenderer>().material;
        curModelMaterial.shader = shaderT;
        curModelColor = curModelMaterial.color;
        curModelColor.a = 0;
        curModelMaterial.color = curModelColor;
        
    }
    //void changeLastModel(GameObject g)
    //{
    //    lastGameObject = g;

    //    lastModelMaterial = g.GetComponent<MeshRenderer>().material;
    //    lastModelMaterial.shader = shader2;
    //    lastModelColor = lastModelMaterial.color;
        
    //}
    public void ClickBook()
    {
        //进入下一个流程
        //comeNextState();
        //AppStatusSwitch(AppState.Show);
        //转到实验室，在协和里转了。以后要不要分离淡出，转向，淡入，这些步骤，有待观察
        selectMenu.transform.parent.Find("Tips").gameObject.SetActive(false);
        selectMenu.SetActive(false);
        endMenu.SetActive(true);
        StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
    }
    
    //void TransGameObject()
    //{
    //    if (curState != SYSState.EggLaying)
    //    //将上一个模型变透明
    //    {
    //        if (isLastModelAlpha)
    //        {
    //            if (lastModelMaterial != null)
    //            {
    //                lastModelColor.a = Mathf.Lerp(lastModelColor.a, 0, 0.02f);
    //                lastModelMaterial.color = lastModelColor;
    //                if (lastModelColor.a < 0.05f)
    //                {
    //                    isLastModelAlpha = false;
    //                    lastModelColor.a = 0f;
    //                    lastModelMaterial.color = lastModelColor;
    //                    lastGameObject.SetActive(false);
    //                }
    //            }
    //        }
    //    }
    //    //将当前状态模型显示
    //    if (SYSState.EggLaying != curState)
    //    {
    //        if (isCurModelAlpha)
    //        {
    //            curModelColor.a = Mathf.Lerp(curModelColor.a, 1, 0.02f);
    //            curModelMaterial.color = curModelColor;
    //            if (curModelColor.a > 0.95f)
    //            {
    //                isCurModelAlpha = false;
    //                curModelColor.a = 1f;
    //                curModelMaterial.color = curModelColor;
    //                curModelMaterial.shader = shader1;
    //            }
    //        }
    //    }
    //}
    
    //public void comeNextState()
    //{
    //    //将上一个状态的模型的颜色和材质数据保存出来
    //    if (dicGameObject.ContainsKey(curState))// && sState != SYSState.FuHQ)
    //    {
    //        //changeLastModel(dicGameObject[curState]);
    //    }
    //    int a = (int)curState;
    //    a++;
    //    curState = (SYSState)(a);
    //    if (curState == SYSState.FuHQ) 
    //        isFlowStart = false;
		

    //    //将当前状态的模型的颜色和材质数据保存出来
    //    if (dicGameObject.ContainsKey(curState))
    //    {
    //        if(curState != SYSState.MiaoJ1)
    //            changeCurModel(dicGameObject[curState]);
    //    }
    //}

    public void ResetState()
    {
        isFlowStart = true;
        StopAllCoroutines();
        isFadeIn = isFadeOut = false;
        HideContent();
	    isFlowEnterDone= false;
	    isFlowExecDone = false;
	    isFlowEnd = false;
    }
	public void TransWeek(string s, string con)
    {
        //生成一个新的周数项
        GameObject g = Instantiate(Resources.Load<GameObject>("Week"));
        g.transform.parent = weekContent;
        g.transform.localScale = weekContent.localScale;
        g.GetComponent<WeekEffect>().setPosition(s, con);
        //通知上升翻页
        weekContent.BroadcastMessage("setTrans",SendMessageOptions.DontRequireReceiver);
    }
    public void HideContent()
    {
        contentTemp.gameObject.SetActive(false);
        contentHumi.gameObject.SetActive(false);
        contentOther.gameObject.SetActive(false);
        contentDes.gameObject.SetActive(false);
    }
    public void contentDisplay(string[] con)
    {
        //显示内容
        for (int i = 0; i < con.Length; i++)
        {
            if(i > 0 && con.Length - 1 == i)
            {
                contentDes.gameObject.SetActive(true);
                contentDes.text = con[i];
            }
            if(i == 1 && i > 0 && i < con.Length - 1)
            {
                contentTemp.gameObject.SetActive(true);
                contentTemp.text = con[i];
            }
            if (i == 2 && i > 0 && i < con.Length - 1)
            {
                contentHumi.gameObject.SetActive(true);
                contentHumi.text = con[i];
            }
            if (i == 3 && i > 0 && i < con.Length - 1)
            {
                contentOther.gameObject.SetActive(true);
                contentOther.text = con[i];
            }
        }
    }

    public void AppStatusSwitch(AppState s)
    {
        curAppStatus = s;
    }
    public void StageStatusSwitch()
    {
        int s = (int)curStageStatus;
        s++;
        curStageStatus = (StageState)s;
        if (s == ConfigData.Instance.Data.Count - 2)
        {
            //TODO:执行到流程结束的事件
            if(FlowEndEvent != null)
            {
                FlowEndEvent();
            }
            s++;
            curStageStatus = (StageState)s;
            //TODO:执行到流程结束的事件
            if (FlowEndEvent != null)
            {
                FlowEndEvent();
            }
        }
        else if(s < ConfigData.Instance.Data.Count - 2)
        {
            //TODO:执行到流程结束的事件
            if (FlowEndEvent != null)
            {
                FlowEndEvent();
            }
        }
    }
    public void StageStatusSwitch(StageState ss)
    {
        curStageStatus = ss;
        StageStatusReset();
    }
    public void StageStatusReset()
    {
        if (playStatus == PlayState.auto)
        {
            isFlowStart = true;
        }
        else
        {
            isFlowStart = false;
        }
        isFlowEnd = false;
        isFlowEnterDone = false;
        isFlowExecDone = false;
        StopAllCoroutines();
        HideContent();
    }
}
