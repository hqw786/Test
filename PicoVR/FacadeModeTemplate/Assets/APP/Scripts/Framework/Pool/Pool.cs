using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class Pool : Singelation<Pool>
{
    //定义子对象池字典
    Dictionary<string, SubPool> Pools = new Dictionary<string, SubPool>();
    
    //子对象池的增、删、查
    //查：根据string获得一个子对象池中的对象(第一次多传一个参数TRUE，表示要加载预置体，以后默认为FALSE）
    public GameObject GetObject(string subPoolName)
    {
        return Pools[subPoolName].GetOneObject();
    }
    //查：
    public List<GameObject> GetAllObjects(string subPoolName)
    {
        return Pools[subPoolName].GetAllObjects();
    }
    //查：
    public List<GameObject> GetActiveObjects(string subPoolName)
    {
        return Pools[subPoolName].GetActiveObjects();
    }
    //增：根据子对象池名称增加一组新的子对象池。
    public void AddPool(string subPoolName)
    {
        Pools.Add(subPoolName, new SubPool());
        //加载预置体
        Pools[subPoolName].LoadPrefab(subPoolName);
        //预生成一些对象备用
        Pools[subPoolName].GetOneObject();
    }
    //删：
    public void HidePool(string subPoolName)
    {
        Pools[subPoolName].HideAllObjects();
    }
    //删：
    public void HidePools()
    {
        foreach(SubPool sp in Pools.Values)
        {
            sp.HideAllObjects();
        }
    }
    //删：
    public void ClearPool(string subPoolName)
    {
        Pools[subPoolName].ClearAllObjects();
    }
    //删：
    public void ClearPools()
    {
        foreach(SubPool sp in Pools.Values)
        {
            sp.ClearAllObjects();
        }
    }
    //
    //
}
