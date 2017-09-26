using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
