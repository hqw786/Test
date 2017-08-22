using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class View : MonoBehaviour 
{
    public abstract string Name { get; }
    
    public List<string> attentionEvents = new List<string>();
    
    public virtual void RegisterEvents()
    {
        
    }
    protected T GetModel<T>() where T : Model
    {
        return MVC.GetModel<T>() as T;
    }
    protected T GetView<T>() where T : View
    {
        return MVC.GetView<T>() as T;
    }
    public abstract void HandleEvent(string eventName, object data);
    protected void SendEvent(string eventName, object data = null)
    {
        MVC.SendEvent(eventName, data);
    }
}
