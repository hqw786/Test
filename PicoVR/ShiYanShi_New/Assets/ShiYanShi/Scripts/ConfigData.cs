using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigData : MonoBehaviour {
    public static ConfigData Instance;

    [HideInInspector]
    public List<string[]> FuHQ = new List<string[]>();
    [HideInInspector]
    public List<string[]> MiaoJ1 = new List<string[]>();
    [HideInInspector]
    public List<string[]> MiaoJ2 = new List<string[]>();
    [HideInInspector]
    public List<string[]> QingNJ = new List<string[]>();
    [HideInInspector]
    public List<string[]> ChengNJ = new List<string[]>();
    [HideInInspector]
    public List<string[]> ChanDJ = new List<string[]>();
    [HideInInspector]
    public List<string[]> DAN = new List<string[]>();

	// Use this for initialization
    void Awake()
    {
        Instance = this;
        Tools.GetTextData("TextData");

    }
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
