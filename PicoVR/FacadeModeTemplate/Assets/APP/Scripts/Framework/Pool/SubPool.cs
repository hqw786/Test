using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SubPool
{
    //预置体对象
    GameObject prefab;
    //实例化对象集合
    List<GameObject> objects = new List<GameObject>();
    ////属性预置体是否为空
    //public bool PrefabIsNull
    //{
    //    get
    //    {
    //        return prefab == null;
    //    }
    //}
    //设置预置体
    public void LoadPrefab(string name)
    {
        if(prefab == null) prefab = Resources.Load<GameObject>("Prefabs/Monsters/" + name);
        else
        {
            Debug.LogError("此子对象池也存在，请检查是否重复增加了相同的子对象池");
        }
    }
    //预置体实例化方法，实例化并添加到集合中(实例化数量由参数决定，并返回第一个实例化对象）
    private GameObject SpawnObject(int num)
    {
        GameObject temp = null;
        if(num <= 0) return null;
        for (int i = 0; i < num; i++)
        {
            GameObject g = GameObject.Instantiate(prefab);
            g.SetActive(false);
            objects.Add(g);
            if(i == 0)
            {
                temp = g;
            }
        }
        return temp;
    }
    //从集合中选出一个隐藏对象，设为显示并返回
    public GameObject GetOneObject()
    {
        foreach(GameObject g in objects)
        {
            if(!g.activeInHierarchy)
            {
                return g;
            }
        }
        //如果没有隐藏对象，则生成新的对象并返回。
        return SpawnObject(Consts.SPAWN_OBJECT_NUMBER);
    }
    //返回全部对象
    public List<GameObject> GetAllObjects()
    {
        return objects;
    }
    //返回全部显示对象
    public List<GameObject> GetActiveObjects()
    {
        List<GameObject> temp = new List<GameObject>();
        foreach(GameObject g in objects)
        {
            if(g.activeInHierarchy)
            {
                temp.Add(g);
            }
        }
        return temp.Count > 0 ? temp : null;
    }
    //对象集合全部隐藏
    public void HideAllObjects()
    {
        foreach(GameObject g in objects)
        {
            g.SetActive(false);
        }
    }
    //清空集合
    public void ClearAllObjects()
    {
        objects.Clear();
    }
    //
}
