using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseModel
{
    protected int ID { get; set; }
    public abstract string Name { get; }
    protected string Des { get; set; }
    
    //protected BaseModel(int id, string name, string des)
    //{
    //    ID = id;
    //    Name = name;
    //    Des = des;
    //}

    //protected BaseModel(int id, string name):this(id,name,string.Empty)
    //{

    //}

    //protected BaseModel(int id):this(id,string.Empty,string.Empty)
    //{

    //}

    protected void SendEvent(string eventName,object data = null)
    {
        Debug.Log("BaseModel.SendEvent:  " + eventName);
        MVC.SendEvent(eventName, data);
    }
}
