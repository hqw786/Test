using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StageModel : BaseModel
{
    List<StageInfo> stageList = new List<StageInfo>();

    public override string Name
    {
        get { return Consts.M_StageModel; }
    }
    

    public void Init()
    {
        //将TXT文本内容保存到stageList中
        Debug.Log("StageModel.Init");
    }

    
}
