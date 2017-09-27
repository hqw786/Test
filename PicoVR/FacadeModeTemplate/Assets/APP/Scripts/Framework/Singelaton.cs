using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这种只能对需要继承MONOBEHAVIOR才有用
public class Singelation<T> : MonoBehaviour where T : MonoBehaviour
{
    private T instance;
	// Use this for initialization
	public T Instance
    {
        get
        {
            if (instance == null) instance = this as T;
            return instance;
        }
    }
}
