using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//对模型数据进行处理
public class BaseProxy <T>where T: ModelBase,new()  {

    protected  List<T> modelList;

    public BaseProxy()
    {
        modelList = new List<T>();
    }

    //尝试获得模型数据
    public bool TryGetModel(int id, out T model)
    {
        model = this.modelList.FirstOrDefault(a => a.ID == id);//返回a相当于T a，List里的每个元素，

        if (model == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //改  更新模型数据
    public void Update(T model)
    {
        T tmpModel = this.GetModelById(model.ID);
        tmpModel = model;
    }


    //查  获得模型数据列表
    public List<T> GetModelList()
    {
        return this.modelList;
    }
    //增
    public void AddModelToList(T model)
    {
        this.modelList.Add(model);
    }
    //改
    //public void UpdateModel(T model)
    //{
    //    T tmpModel = this.GetModelById(model.ID);
    //    tmpModel = model;
    //}

    //查数量
    public int GetMaxId()
    {
        if (this.modelList.Count==0)
        {
            return 0;
        }
        return this.modelList.Max(a => a.ID);
    }
    //查  通过ID获得模型模型数据
    public T GetModelById(int id)
    {
        return modelList.FirstOrDefault(a => a.ID==id);
    }
}
