using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

//这个挂到UI上

public class PackCompent:MonoBehaviour
{
    public static PackCompent Intance;

    public void Awake()
    {
        Intance = this;
    }


    //清空再生成物品框
    public void ShowPack(List<PackModel> modelList)
    {
        //删除父物体下面的所有子物体
        while (this.transform.childCount != 0)
        {
            GameObject.DestroyImmediate(this.transform.GetChild(0).gameObject);
        }
        //生成指定数量的子物体
        foreach (var item in modelList)
        {
            GameObject obj = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("PackItem"));
            obj.transform.parent = this.transform;
            obj.transform.localScale = Vector3.one;
            PackItem packItem = obj.GetComponent<PackItem>();
            packItem.Model = item;
        }
    }
}
