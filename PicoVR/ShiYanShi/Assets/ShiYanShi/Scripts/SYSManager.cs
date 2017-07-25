using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Pvr_UnitySDKAPI;
public enum SYSState
{
	Init,
    Select,
    FuHQ,
    MiaoJ1,
    MiaoJ2,
    QingNJ,
    ChengNJ,
    ChanDJ,
    EggLaying,
    EggShow,
    END
}
public class SYSManager : MonoBehaviour 
{
    public static SYSManager Instance;
    public const float FADETIME = 1.5f;
    public const float CONTENTTIME = 3f;
    public const float CONTENTALPHATIME = 1f;
    public Dictionary<SYSState, GameObject> dicGameObject = new Dictionary<SYSState, GameObject>();
	public Dictionary<SYSState, List<GameObject>> dicEgg = new Dictionary<SYSState, List<GameObject>>();
    public Dictionary<SYSState, List<string[]>> dicContent = new Dictionary<SYSState, List<string[]>>();
    public Dictionary<string, string[]> dicEggContent = new Dictionary<string, string[]>();

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
    public bool isStateStart;//一个新状态开始
    public bool isLastModelAlpha;//可以将上一个模型变透明
    public bool isCurModelAlpha;//可以将当前状态模型显示
    public bool isTempModelAlpha;//临时模型显示
    public bool isModelAlphaDone;
    public bool isContentAlphaHide;
    public bool isContentAlphaDisplay;

	public bool isStateEnterDone;
	public bool isStateExecDone;
	public bool isStateExitDone;

    bool isMenuTips;
    bool isEggTips;
    bool isVR90First;

    Color curModelColor;
    Color lastModelColor;
    Material curModelMaterial;
    Material lastModelMaterial;
    GameObject lastGameObject;
    GameObject curGameObject;

    BoxCollider bcBook;

    public SYSState curState;
	private SYSState lastState;

    //Text contentWeek;
    Text contentTemp;
    Text contentHumi;
    Text contentOther;
    Text contentDes;

    bool isFirst;

    public Shader shader1;
    public Shader shader2;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
	    lastState = SYSState.Init;
        curState = SYSState.Select;
        endMenu.SetActive(false);

        shader1 = Shader.Find("Mobile/Diffuse");
        shader2 = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        //contentWeek = content.transform.parent.Find("Week").GetComponent<Text>();
        contentTemp = content.transform.Find("Temp").GetComponent<Text>();
        contentHumi = content.transform.Find("Humi").GetComponent<Text>();
        contentOther = content.transform.Find("Other").GetComponent<Text>();
        contentDes = content.transform.Find("Des").GetComponent<Text>();
        GameObject g = Resources.Load<GameObject>("ji/jidan_02");
        dicGameObject.Add(SYSState.FuHQ, Instantiate(g));
        dicGameObject[SYSState.FuHQ].SetActive(false);

        g = Resources.Load<GameObject>("ji/muji_01");
        dicGameObject.Add(SYSState.MiaoJ1, Instantiate(g));
        dicGameObject[SYSState.MiaoJ1].SetActive(false);

        g = Resources.Load<GameObject>("ji/muji_02");
        dicGameObject.Add(SYSState.MiaoJ2, Instantiate(g));
        dicGameObject[SYSState.MiaoJ2].SetActive(false);

        g = Resources.Load<GameObject>("ji/muji_03");
        dicGameObject.Add(SYSState.QingNJ, Instantiate(g));
        dicGameObject[SYSState.QingNJ].SetActive(false);

        g = Resources.Load<GameObject>("ji/muji_04");
        dicGameObject.Add(SYSState.ChengNJ, Instantiate(g));
        dicGameObject[SYSState.ChengNJ].SetActive(false);

        g = Resources.Load<GameObject>("ji/muji_05");
        dicGameObject.Add(SYSState.ChanDJ, Instantiate(g));
        dicGameObject[SYSState.ChanDJ].SetActive(false);

        //鸡蛋模型
        List<GameObject> egg = new List<GameObject>();
        g = Resources.Load<GameObject>("ji/jidan_02");
        egg.Add(Instantiate(g));
        g = Resources.Load<GameObject>("ji/jidan_03");
        egg.Add(Instantiate(g));
        g = Resources.Load<GameObject>("ji/jidan_01");
        egg.Add(Instantiate(g));
        dicEgg.Add(SYSState.EggShow, egg);
        dicEgg[SYSState.EggShow][0].SetActive(false);
        dicEgg[SYSState.EggShow][1].SetActive(false);
        dicEgg[SYSState.EggShow][2].SetActive(false);
		

	    dicContent.Add(SYSState.FuHQ, ConfigData.Instance.FuHQ);
		dicContent.Add(SYSState.MiaoJ1, ConfigData.Instance.MiaoJ1);
        dicContent.Add(SYSState.MiaoJ2, ConfigData.Instance.MiaoJ2);
		dicContent.Add(SYSState.QingNJ, ConfigData.Instance.QingNJ);
		dicContent.Add(SYSState.ChengNJ, ConfigData.Instance.ChengNJ);
		dicContent.Add(SYSState.ChanDJ, ConfigData.Instance.ChanDJ);

        //鸡蛋数据
		dicEggContent.Add("jidan_02", ConfigData.Instance.DAN[0]);
        dicEggContent.Add("jidan_03", ConfigData.Instance.DAN[1]);
        dicEggContent.Add("jidan_01", ConfigData.Instance.DAN[2]);
    }
	void Start () {
        //isEggTips = PlayerPrefs.GetInt("isEggTips") == 1 ? true : false;
        //isMenuTips = PlayerPrefs.GetInt("isMenuTips") == 1 ? true : false;
        
        //将摄像头归为原始位（rotation(0,0,0));
        float angle = Vector3.Angle(leftEye.transform.parent.parent.forward, Vector3.forward);
        if(angle<45)
        {
            leftEye.transform.parent.parent.rotation = Quaternion.identity;
            leftEye.transform.parent.parent.eulerAngles = new Vector3(0, -90, 0);
        }
        //print(angle);
        //angle = Vector3.Angle(leftEye.transform.parent.parent.forward, Vector3.forward);
        //print(angle);
        //else
        //{

        //}

        //if (!Manager.isFirst)
        //{
        //    Manager.isFirst = true;
        //    leftEye.transform.parent.parent.rotation = Quaternion.identity;
        //    leftEye.transform.parent.parent.eulerAngles = new Vector3(0, -90, 0);
        //}
        //else
        //{
            
        //}
        
        
        if (Controller.UPvr_GetControllerState() == ControllerState.Connected)
        {
            gazePointer.SetActive(false);
        }

        if(!Manager.isMenuTips)
        {
            Manager.isMenuTips = true;
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

        //Pvr_UnitySDKAPI.Sensor.UPvr_ResetSensor((int)Pvr_UnitySDKAPI.Sensorindex.Default);
        //重置传感器（以当前方向为原始位置）。
        if (Pvr_UnitySDKManager.SDK != null)
        {
            Pvr_UnitySDKManager.pvr_UnitySDKSensor.ResetUnitySDKSensor();
        }

        selectMenu.SetActive(true);
        endMenu.SetActive(false);
        SceneManager.LoadScene(0);
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

	public void FuHuaXiangAndTV()
	{
		Debug.Log("孵化箱上升");
		fuhuaxiang.parent.GetComponent<Animation>().CrossFade("fuhuaxiang", 3f);
		//电视开机(这边以后可以做成渐渐开机效果，显示LOGO）
		Debug.Log("屏幕打开");
		content.transform.parent.GetComponent<PingMuTrans>().SetDisplay();
	}

	void Update () {
        //if(!isFirst &&　Time.time > 5f)
        //{
        //    isFirst = true;
        //    ClickBook(book);
        //}
        //到了最后
        //if (curState == SYSState.END)
        //{
        //    //TODO:再看一遍显示出来
        //    endMenu.transform.Find("Restart").gameObject.SetActive(true);
        //    contentDes.gameObject.SetActive(true);
        //    isContentAlphaDisplay = true;
        //    contentDes.text = "谢谢观看！";
        //    return;
        //}
        //到了产蛋阶段
        if(curState == SYSState.EggLaying || curState == SYSState.EggShow)
        {
            isStateStart = true;//这个设置为真就不会进么状态循环
            if(curState == SYSState.EggLaying)
                eggLayingShow();
        }
		//进入状态前
		if (!isStateStart)
		{
			print("进入" + curState + "状态");
			isStateStart = true;
            isStateEnterDone = false;
            isStateExecDone = false;
            isStateExitDone = false;
			//协和执行,完成后isStateEnterDone为True
			StartCoroutine(EnterState());
		}

		//执行状态中
		if (isStateEnterDone)
		{
			print(curState + "状态");
			isStateEnterDone = false;
			//协和执行，完成后isStateExecDone为True
			if(curState != SYSState.Select)
				StartCoroutine(ExecState());
		}

		//退出状态
		if (isStateExecDone)
		{
			print("退出" + curState + "状态");
			isStateExecDone = false;
			
			//切换状态
			print("切换到下一个状态");
			comeNextState();

			//协和执行，完成后isStateExitDone为True
			StartCoroutine(ExitState());
		}

		//切换状态
		if (isStateExitDone)
		{
            isStateEnterDone = false;
			isStateStart = false;
            print("可以进入下一个状态");
		}
        //模型切换效果
		TransGameObject();
		#region 调试完成就删除
		//switch(curState)
		//{
		//	case SYSState.Select:
		//		{
		//			//第一个状态比较特殊。
		//			//SelectState();
		//		}
		//		break;
		//	case SYSState.FuHQ:
		//		{
		//			FuHQStateTrans();
		//			//孵化箱动画
		//			if (!isStateStart)
		//			{
		//				//淡入淡出效果
		//				StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
		//				isStateStart = true;
		//				//播放孵化箱上升和下降动画
		//				fuhuaxiang.transform.parent.GetComponent<Animation>().CrossFade("fuhuaxiang", 3f);
		//				//电视开机(这边以后可以做成渐渐开机效果，显示LOGO）
		//				//content.transform.parent.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load<Texture>("Materials/glass");
		//				//content.transform.parent.GetComponent<PingMuTrans>().SetDisplay();

		//				//invoke延时执行下一个过程
		//				Invoke("FuHQState", 8.5f);
		//			}
		//			TransGameObject();
		//		}
		//		break;
		//	case SYSState.MiaoJ:
		//		{
		//			//淡入淡出效果
		//			MiaoJStateTrans();
		//			if(!isStateStart)
		//			{
		//				//淡入淡出效果
		//				//StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
		//				isStateStart = true;
		//				//invoke延时执行下一个过程
		//				Invoke("MiaoJState", 3f);
		//			}
		//			TransGameObject();
		//		}
		//		break;
		//	case SYSState.QingNJ:
		//		{
		//			QingNJStateTrans();
		//			if (!isStateStart)
		//			{
		//				//淡入淡出效果
		//				//StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
		//				isStateStart = true;
		//				//invoke延时执行下一个过程
		//				Invoke("QingNJState", 3f);
		//			}
		//			TransGameObject();
		//		}
		//		break;
		//	case SYSState.ChengNJ:
		//		{
		//			ChengNJStateTrans();
		//			if (!isStateStart)
		//			{
		//				//淡入淡出效果
		//				//StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
		//				isStateStart = true;
		//				//invoke延时执行下一个过程
		//				Invoke("ChengNJState", 3f);
		//			}
		//			TransGameObject();
		//		}
		//		break;
		//	case SYSState.ChanDJ:
		//		{
		//			ChanDJStateTrans();
		//			if (!isStateStart)
		//			{
		//				//淡入淡出效果
		//				//StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
		//				isStateStart = true;
		//				//invoke延时执行下一个过程
		//				Invoke("ChanDJState", 3f);
		//			}
		//			TransGameObject();
		//		}
		//		break;
		//	case SYSState.DAN1:
		//		{
		//			DANStateTrans();
		//			if (!isStateStart)
		//			{
		//				//淡入淡出效果
		//				//StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
		//				isStateStart = true;
		//				//invoke延时执行下一个过程
		//				Invoke("DANState", 3f);
		//			}
		//			TransGameObject();
		//		}
		//		break;
		//	case SYSState.DAN2:
		//		{
		//			DANStateTrans();
		//			if (!isStateStart)
		//			{
		//				//淡入淡出效果
		//				//StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
		//				isStateStart = true;
		//				//invoke延时执行下一个过程
		//				Invoke("DANState", 3f);
		//			}
		//			TransGameObject();
		//		}
		//		break;
		//	case SYSState.DAN3:
		//		{
		//			DANStateTrans();
		//			if (!isStateStart)
		//			{
		//				//淡入淡出效果
		//				//StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
		//				isStateStart = true;
		//				//invoke延时执行下一个过程
		//				Invoke("DANState", 3f);
		//			}
		//			TransGameObject();
		//		}
		//		break;
		//}
		#endregion
	}

	private IEnumerator EnterState()
	{
		if (curState == SYSState.FuHQ)
		{
			yield return StartCoroutine(Tools.FadeInFadeOut(leftEye, rightEye, FADETIME));
            fuhuaxiang.parent.GetComponent<Animation>().CrossFade("fuhuaxiang_001");
			content.transform.parent.GetComponent<PingMuTrans>().SetDisplay();
			yield return new WaitForSeconds(4f);
			SYSManager.Instance.isCurModelAlpha = true;
		}
		yield return new WaitForSeconds(1f);
		isStateEnterDone = true;
	}

	IEnumerator ExecState()
	{
		yield return StartCoroutine(Tools.DisplayContent(dicContent[curState], CONTENTTIME, CONTENTALPHATIME));
		yield return new WaitForSeconds(1f);
		isStateExecDone = true;
	}

	IEnumerator ExitState()
	{
		if (curState == SYSState.MiaoJ1)
		{
            //箱子降下去
			fuhuaxiang.parent.GetComponent<FuHuaXiang>().continuePlay();
			//TODO:加播一段动画 AddAnAnimation(GAMEOBJECT A,GAMEOBJECT B)
			//过渡效果从一个渐隐渐现过程(蛋，孵化，苗鸡)
            if (tempObject == null)
            {
                tempObject = Instantiate(Resources.Load<GameObject>("ji/jidan_04"));
            }
            changeCurModel(tempObject);
		}

		yield return StartCoroutine(Tools.TransModelAlpha(1f));
        if(curState != SYSState.MiaoJ1)
            isStateExitDone = true;
        
		if (curState == SYSState.MiaoJ1)
        {
            yield return new WaitForSeconds(5f);
            changeLastModel(tempObject);
            changeCurModel(dicGameObject[curState]);
            yield return StartCoroutine(Tools.TransModelAlpha(1f));
            yield return new WaitForSeconds(3f);
            isStateExitDone = true;
        }
        yield return new WaitForSeconds(3f);
		//isStateExitDone = true;
	}
    //产蛋展示
    void eggLayingShow()
    {
        //过程：显示UI，点击显示蛋分类，点击不同的蛋显示相应的介绍
        //周数隐藏，
        weekContent.gameObject.SetActive(false);
        //显示产蛋UI
        eggLaying.transform.Find("eggLaying").gameObject.SetActive(true);
        dicGameObject[SYSState.ChanDJ].GetComponent<MeshRenderer>().material.shader = shader1;
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
        dicGameObject[SYSState.ChanDJ].GetComponent<MeshRenderer>().material.shader = shader1;
        eggLaying.transform.Find("eggLaying").BroadcastMessage("SetShadingHide", SendMessageOptions.DontRequireReceiver);
        curState = SYSState.EggShow;
        eggLaying.transform.Find("Tips").gameObject.SetActive(false);
        modelPoint.transform.parent.GetComponent<Around>().enabled = false;
        //蛋种类Panel渐渐显示,鸡模型隐藏，三种蛋显示出来
        //eggLaying.transform.Find("Egg").gameObject.SetActive(true);
        //eggLaying.transform.Find("Egg").BroadcastMessage("SetShadingDisplay",SendMessageOptions.DontRequireReceiver);
        dicGameObject[SYSState.ChanDJ].SetActive(false);
        //
        weekContent.gameObject.SetActive(true);
        foreach(Transform t in weekContent)
        {
            t.gameObject.SetActive(false);
        }
        GameObject gg = Instantiate(Resources.Load<GameObject>("Week"));
        gg.transform.parent = weekContent;
        gg.transform.localScale = weekContent.localScale;
        gg.GetComponent<WeekEffect>().setPosition("Mid", "选择鸡蛋了解产蛋信息");

        int index = -1;
        modelPoint.transform.parent.rotation = Quaternion.identity;
        foreach(GameObject g in dicEgg[SYSState.EggShow])
        {
            g.SetActive(true);
            g.transform.parent = modelPoint.transform;
            g.transform.rotation = Quaternion.identity;
            g.transform.localPosition = Vector3.zero + new Vector3(0.15f * index, 0f, 0f);
            g.transform.localScale = modelPoint.transform.localScale;
            g.AddComponent<BoxCollider>();
            index++;
        }
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
        curModelMaterial.shader = shader2;
        curModelColor = curModelMaterial.color;
        curModelColor.a = 0;
        curModelMaterial.color = curModelColor;
        
    }
    void changeLastModel(GameObject g)
    {
        lastGameObject = g;

        lastModelMaterial = g.GetComponent<MeshRenderer>().material;
        lastModelMaterial.shader = shader2;
        lastModelColor = lastModelMaterial.color;
        
    }
    public void ClickBook()
    {
        //进入下一个流程
        comeNextState();
        //转到实验室，在协和里转了。以后要不要分离淡出，转向，淡入，这些步骤，有待观察
        selectMenu.transform.parent.Find("Tips").gameObject.SetActive(false);
        selectMenu.SetActive(false);
        endMenu.SetActive(true);
    }
    
    void TransGameObject()
    {
        if (curState != SYSState.EggLaying)
        //将上一个模型变透明
        {
            if (isLastModelAlpha)
            {
                if (lastModelMaterial != null)
                {
                    lastModelColor.a = Mathf.Lerp(lastModelColor.a, 0, 0.02f);
                    lastModelMaterial.color = lastModelColor;
                    if (lastModelColor.a < 0.05f)
                    {
                        isLastModelAlpha = false;
                        lastModelColor.a = 0f;
                        lastModelMaterial.color = lastModelColor;
                        lastGameObject.SetActive(false);
                    }
                }
            }
        }
        //将当前状态模型显示
        if (SYSState.EggLaying != curState)
        {
            if (isCurModelAlpha)
            {
                curModelColor.a = Mathf.Lerp(curModelColor.a, 1, 0.02f);
                curModelMaterial.color = curModelColor;
                if (curModelColor.a > 0.95f)
                {
                    isCurModelAlpha = false;
                    curModelColor.a = 1f;
                    curModelMaterial.color = curModelColor;
                    curModelMaterial.shader = shader1;
                }
            }
        }
    }
    
    public void comeNextState()
    {
        //将上一个状态的模型的颜色和材质数据保存出来
        if (dicGameObject.ContainsKey(curState))// && sState != SYSState.FuHQ)
        {
            changeLastModel(dicGameObject[curState]);
        }
	    lastState = curState;
        int a = (int)curState;
        a++;
        curState = (SYSState)(a);
        if (curState == SYSState.FuHQ) 
            isStateStart = false;
		

        //将当前状态的模型的颜色和材质数据保存出来
        if (dicGameObject.ContainsKey(curState))
        {
            if(curState != SYSState.MiaoJ1)
                changeCurModel(dicGameObject[curState]);
        }
    }

    public void ResetState()
    {
        curState = SYSState.Select;
	    //lastState = SYSState.Select;
        isStateStart = true;
        StopAllCoroutines();
        isFadeIn = isFadeOut = false;
        HideContent();
	    isStateEnterDone = false;
	    isStateExecDone = false;
	    isStateExitDone = false;
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
        //contentWeek.gameObject.SetActive(false);
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
            //if(i == 0)
            //{
            //    contentWeek.gameObject.SetActive(true);
            //    contentWeek.text = con[0];
            //}
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
}
