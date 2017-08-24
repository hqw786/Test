using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Games : ApplicationBase<Games>
{
    
    //对象池
    public Pool pool;
    //声音
    public Sound sound;
    //设置数据
    public ConfigData configData;
    //主状态
    public MainGameStatus mainStatus;
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

        MainStatusSwitch(MainGameStatus.showMyLogo);
    }
	public void MainStatusSwitch(MainGameStatus mgs)
    {
        //
        mainStatus = mgs;
        switch(mgs)
        {
            case MainGameStatus.showMyLogo:
                {
                    LoadScene(1);
                }
                break;
            case MainGameStatus.showCompanyLogo:
                {
                    LoadScene(2);
                }
                break;
            case MainGameStatus.title:
                {
                    LoadScene(3);
                }
                break;
            case MainGameStatus.menu:
                {
                    LoadScene(4);
                }
                break;
            case MainGameStatus.level:
                {
                    LoadScene(5);
                }
                break;
            case MainGameStatus.end:
                {

                }
                break;
        }
    }
    void LoadScene(int index)
    {
        //
        SceneArgs sa = new SceneArgs();
        sa.SceneIndex = SceneManager.GetActiveScene().buildIndex;
        SendEvent(Consts.C_ExitScene, sa);
        SceneManager.LoadScene(index);
    }
    void OnLevelWasLoaded(int level)
    {
        //
        SceneArgs sa = new SceneArgs();
        sa.SceneIndex = level;
        SendEvent(Consts.C_EnterScene, sa);
        FirstDoingOfLoaded();
    }
    void FirstDoingOfLoaded()
    {
        switch (mainStatus)
        {
            case MainGameStatus.showMyLogo:
                {
                    SendEvent(Consts.C_ShowMyLogo);
                }
                break;
            case MainGameStatus.showCompanyLogo:
                {
                    SendEvent(Consts.C_ShowCompanyLogo);
                }
                break;
            case MainGameStatus.title:
                {
                    SendEvent(Consts.C_ShowTitle);
                }
                break;
            case MainGameStatus.menu:
                {

                }
                break;
            case MainGameStatus.level:
                {
                    SendEvent(Consts.C_SpawnEgg);
                }
                break;
            case MainGameStatus.end:
                {

                }
                break;
        }
    }
}
