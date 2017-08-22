using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : ApplicationBase<Pool>
{
    Dictionary<string, SubPool> dicSubPools = new Dictionary<string, SubPool>();
    void Awake()
    {
        base.Awake();
    }
    //对子对象池:增,删,改,查
    //这个内部调用，增加一个新的子对象池
    void AddSubPool(string prefabName)
    {
        //加载预置体
        GameObject prefab = Resources.Load<GameObject>("Prefabs/" + prefabName);
        dicSubPools[prefabName] = new SubPool(prefab);
    }
    //这个外部调用，删除一个或全部子对象池
    public void DelSubPool()
    {
        //删除全部子对象池
        dicSubPools.Clear();
    }
    public void DelSubPool(string subPoolName)
    {
        //删除此名字的子对象池
        if(dicSubPools.ContainsKey(subPoolName))
        {
            dicSubPools.Remove(subPoolName);
        }
        else
        {
            print("字典中不存在该键值： " + subPoolName);
        }
    }
    //获取一个或全部子对象池
    public SubPool GetSubPool(string prefabName)
    {
        //获得所有子对象池
        return dicSubPools[prefabName];
    }
    public void GetObjectOfPool(string prefabName)
    {
        //获得相应的子对象池
        if (!dicSubPools.ContainsKey(prefabName))
        {
            AddSubPool(prefabName);
        }
        dicSubPools[prefabName].GetObject();
    }

    public GameObject Instantiation(GameObject prefab)
    {
        return Instantiate(prefab);
    }
}
