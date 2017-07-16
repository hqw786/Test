using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigData : MonoBehaviour {
    public static ConfigData Instance;

    [HideInInspector]
    public List<Transform> roamPath = new List<Transform>();

    public List<NodeInfo> pathNodeInfo = new List<NodeInfo>(); 
    
    // Use this for initialization
    void Awake()
    {
        Instance = this;
    }
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
