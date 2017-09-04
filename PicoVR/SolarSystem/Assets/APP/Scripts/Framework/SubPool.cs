using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SubPool
{
    public SubPool()
    {

    }
    public SubPool(GameObject prefab)
    {
        prefabObj = prefab;
    }
    public GameObject prefabObj;
    List<GameObject> objs = new List<GameObject>();
    //对子对象内的对象进行增删改查操作
    GameObject AddObject()
    {
        //实例化预置体并返回
        objs.Add(Pool.Instance.Instantiation(prefabObj));
        return objs[objs.Count - 1];
    }
    public void DelAllObject()
    {
        objs.Clear();
    }
    public GameObject GetObject()
    {
        foreach(GameObject g in objs)
        {
            if(!g.activeInHierarchy)
            {
                return g;
            }
        }
        //运行到这一步，说明没有隐藏的对象，生成新对象
        return AddObject();
    }
}
