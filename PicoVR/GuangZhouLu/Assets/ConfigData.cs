using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoamView
{
    fix,custom
}
public enum MapType
{
    normal,mine
}
public enum WarpName
{
    传送点1,
    传送点2,
    传送点3,
    传送点4,
    传送点5
}
public class ConfigData : MonoBehaviour {
    public static ConfigData Instance;
    public const float fadeTime = 1.5f;
    public const float scaleTime = 1.5f;

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
