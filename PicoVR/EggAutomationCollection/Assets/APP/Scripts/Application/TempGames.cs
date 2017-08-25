using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TempGames : ApplicationBase<TempGames> {
    bool isFirst;

	// Use this for initialization
    void Awake()
    {
        base.Awake();
        RegisterControl(Consts.C_Startup, typeof(StartupCommand));
        SendEvent(Consts.C_Startup);
    }

	void Start () {
        SceneArgs sa = new SceneArgs();
        sa.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        SendEvent(Consts.C_EnterScene, sa);
        
	}
	
	// Update is called once per frame
	void Update () {
        //if(!isFirst)
        //{
        //    isFirst = true;
        //    SendEvent(Consts.C_SpawnEgg);
        //}
	}
}
