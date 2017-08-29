using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigData : MonoBehaviour {
    public static ConfigData Instance;
    public const float fadeTime = 1.5f;
    public const float scaleTime = 0.5f;

    //阶段对应阶段对象(根据现有的阶段生成阶段数量)
    public Dictionary<StageState, FlowBase> dicStage = new Dictionary<StageState, FlowBase>();
    private List<StageInfo> data = new List<StageInfo>();
    //阶段数据
    public List<StageInfo> Data { get { return data; } set { data = value; } }
    //蛋种类
    public Dictionary<StageState, List<GameObject>> dicEgg = new Dictionary<StageState, List<GameObject>>();
    public Dictionary<string, string[]> dicEggContent = new Dictionary<string, string[]>();
    //喂食种类
    public Dictionary<StageState, List<GameObject>> dicFodder = new Dictionary<StageState, List<GameObject>>();
    public Dictionary<string, string[]> dicFodderContent = new Dictionary<string, string[]>();
   
    //现有的阶段(和TXT文本中保持一致）
    public string[] strStage = new string[] { "孵化期", "破壳期", "苗鸡期", "雏鸡期", "青年期", "成年期", "产蛋期", "喂食期", "蛋种类" };// 
	// Use this for initialization
    void Awake()
    {
        Instance = this;
        Tools.GetTextData("TextData");

        SetEggModel();
        //SetFodderModel();
    }
    void SetEggModel()
    {
        //鸡蛋模型
        List<GameObject> egg = new List<GameObject>();
        GameObject g = Resources.Load<GameObject>("ji/jidan_02");
        egg.Add(Instantiate(g));
        g = Resources.Load<GameObject>("ji/jidan_03");
        egg.Add(Instantiate(g));
        g = Resources.Load<GameObject>("ji/jidan_01");
        egg.Add(Instantiate(g));
        dicEgg.Add(StageState.egg, egg);
        dicEgg[StageState.egg][0].SetActive(false);
        dicEgg[StageState.egg][1].SetActive(false);
        dicEgg[StageState.egg][2].SetActive(false);
    }
    void SetFodderModel()
    {
        List<GameObject> fodder = new List<GameObject>();
        GameObject g = Resources.Load<GameObject>("ji/fodder1");
        fodder.Add(Instantiate(g));
        g = Resources.Load<GameObject>("ji/fodder2");
        fodder.Add(Instantiate(g));
        g = Resources.Load<GameObject>("ji/fodder3");
        fodder.Add(Instantiate(g));
        dicFodder.Add(StageState.weisq, fodder);
        dicFodder[StageState.weisq][0].SetActive(false);
        dicFodder[StageState.weisq][1].SetActive(false);
        dicFodder[StageState.weisq][2].SetActive(false);
    }
	void Start () 
    {
		SaveStageDataToDIC();
        SetEggData();
        //SetFodderData();
	}
	void SetEggData()
    {
        //鸡蛋数据
        dicEggContent.Add("jidan_02", Data[Data.Count - 1].Context[0]);
        dicEggContent.Add("jidan_03", Data[Data.Count - 1].Context[1]);
        dicEggContent.Add("jidan_01", Data[Data.Count - 1].Context[2]);
    }
    void SetFodderData()
    {
        //饲料数据
        dicFodderContent.Add("fodder1", Data[Data.Count - 2].Context[0]);
        dicFodderContent.Add("fodder2", Data[Data.Count - 2].Context[1]);
        dicFodderContent.Add("fodder3", Data[Data.Count - 2].Context[2]);
    }
	// Update is called once per frame
	void Update () 
    {
		
	}
    void SaveStageDataToDIC()
    {
        foreach(StageInfo si in data)
        {
            if (si.ID < strStage.Length)
            {
                StageState ss = (StageState)si.ID;
                SwitchObject(ss, si);
                dicStage[ss].SetData(si);
                dicStage[ss].SetDefault();
                if(si.MainModel != null)
                    si.MainModel.SetActive(false);
            }
        }
    }
    void SwitchObject(StageState ss,StageInfo si)
    {
        GameObject g = null;
        switch(ss)
        {
            case StageState.fuhq:
                dicStage.Add(ss, new FlowFuHQ());
                g = Resources.Load<GameObject>("ji/jidan_02");
                si.MainModel = Instantiate(g);
                dicStage[ss].enterTransform.Add(SYSManager.Instance.fuhuaxiang);
                dicStage[ss].enterTransform.Add(SYSManager.Instance.content.transform);
                break;
            case StageState.pokq:
                dicStage.Add(ss, new FlowPoKQ());
                g = Resources.Load<GameObject>("ji/jidan_04");
                si.MainModel = Instantiate(g);
                break;
            case StageState.miaojq:
                dicStage.Add(ss, new FlowMiaoJQ());
                g = Resources.Load<GameObject>("ji/muji_01");
                si.MainModel = Instantiate(g);
                break;
            case StageState.chujq:
                dicStage.Add(ss, new FlowChuJQ());
                g = Resources.Load<GameObject>("ji/muji_02");
                si.MainModel = Instantiate(g);
                break;
            case StageState.qingnq:
                dicStage.Add(ss, new FlowQinNQ());
                g = Resources.Load<GameObject>("ji/muji_03");
                si.MainModel = Instantiate(g);
                break;
            case StageState.chengnq:
                dicStage.Add(ss, new FlowChengNQ());
                g = Resources.Load<GameObject>("ji/muji_04");
                si.MainModel = Instantiate(g);
                break;
            case StageState.chandq:
                dicStage.Add(ss, new FlowChanDQ());
                g = Resources.Load<GameObject>("ji/muji_05");
                si.MainModel = Instantiate(g);
                break;
            case StageState.weisq:
                dicStage.Add(ss, new FlowWeiSQ());
                break;
            case StageState.egg:
                dicStage.Add(ss, new FlowEgg());
                break;
        }
    }
}
