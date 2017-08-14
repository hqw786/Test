using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseModel<T>
{
    protected int ID { get; set; }
    protected string Name { get; set; }
    protected string Des { get; set; }

    protected BaseModel(int id)
    {
        ID = id;
    }
    protected BaseModel(int id, string name):this(id)
    {
        Name = name;
    }
    protected BaseModel(int id, string name, string des):this(id,name)
    {
        Des = des;
    }
    public abstract T GetModel();
}
