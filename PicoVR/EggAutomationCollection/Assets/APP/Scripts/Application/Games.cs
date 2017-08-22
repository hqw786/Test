using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Games : ApplicationBase<Games>
{
    //对象池
    public Pool pool;
    //声音
    public Sound sound;
    //设置数据
    public ConfigData configData;
    //
    //
    //
    //
    //
    //
	// Use this for initialization
    void Awake()
    {
        base.Awake();
    }
	void Start () 
    {
        DontDestroyOnLoad(this.gameObject);
        pool = Pool.Instance;
        sound = Sound.Instance;
        configData = ConfigData.Instance;

        RegisterControl(Consts.C_Startup, typeof(StartupCommand));
        SendEvent(Consts.C_Startup);
    }
	

}
