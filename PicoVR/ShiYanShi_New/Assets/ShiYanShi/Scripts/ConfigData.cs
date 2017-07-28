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

    //现有的阶段(和TXT文本中保持一致）
    public string[] strStage = new string[] { "孵化期", "破壳期", "苗鸡期", "雏鸡期", "青年期", "成年期", "产蛋期", "喂食期", "蛋种类" };
	// Use this for initialization
    void Awake()
    {
        Instance = this;
        Tools.GetTextData("TextData");
    }
	void Start () 
    {
		SaveStageDataToDIC();
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
    void SaveStageDataToDIC()
    {
        foreach(StageInfo si in data)
        {
            if (si.ID < strStage.Length - 1)
            {
                StageState ss = (StageState)si.ID;
                SwitchObject(ss, si);
                dicStage[ss].SetData(si);
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
                g = Resources.Load<GameObject>("ji/muji_01");
                si.MainModel = Instantiate(g);
                break;
        }
    }
}
