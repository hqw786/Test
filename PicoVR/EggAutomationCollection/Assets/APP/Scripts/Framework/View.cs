using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour 
{
    public virtual string Name { get; }
    
    public List<string> attentionEvents = new List<string>();
    
    public void RegisterEvents(string eventName)
    {
        attentionEvents.Add(eventName);
    }
}
