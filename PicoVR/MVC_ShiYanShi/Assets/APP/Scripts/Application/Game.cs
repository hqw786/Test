using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Sound))]
public class Game : BaseApplication<Game> //挂到一个不销毁的物体上
{
	//对象池
    //public ObjectPool pool;
    //声音
    [HideInInspector]
    public Sound sound;
    //配置数据
    //public ConfigData configData = null;

    //游戏状态
    [HideInInspector]
    public MainGameStatus mainStatus;

    void Awake()
    {
        base.Awake();
        //初始化
        print("初始化Game--Awake");
    }

    void Start()
    {
        //进入新场景
        print("Game--Start");
        DontDestroyOnLoad(this.gameObject);
        //pool = ObjectPool.Instance;
        sound = Sound.Instance;
        //configData = ConfigData.Instance;

        //注册所有命令和模型
        print("注册所有命令和模型");
        //第一次的启动命令要在这边注册一下，然后调用
        RegisterController(Consts.C_StartUp, new StartUpCommand());
        SendEvent(Consts.C_StartUp);

        MainStatusSwitch(MainGameStatus.StartUp);
    }
    public void LoadScene(int level)
    {
        //退出当前场景，再进入新场景
        SceneArgs e = new SceneArgs();
        e.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        print("执行退出当前场景事件");
        SendEvent(Consts.C_ExitScene, e);
        print("进入新场景");
        SceneManager.LoadScene(level);
    }
    void OnLevelWasLoaded(int level)
    {
        print("场景加载完毕");
        SceneArgs e = new SceneArgs();
        e.sceneIndex = level;
        print("执行进入场景事件");
        SendEvent(Consts.C_EnterScene, e);
        print("进入新场景后最行执行哪个命令");
        ExecuteCommand();
    }
    private void ExecuteCommand()
    {
        //进入场景后，第一个执行的命令
        print("进入  " + mainStatus + "  场景后的第一个执行命令");
        switch (mainStatus)
        {
            case MainGameStatus.StartUp:
                {
                    print("Game.ExecuteCommand:  " + Consts.C_ShowMyLogo);
                    SendEvent(Consts.C_ShowMyLogo);
                }
                break;
            case MainGameStatus.Logo:
                {
                    SendEvent(Consts.C_ShowCompanyLogo);
                }
                break;
            case MainGameStatus.Menu:
                {
                    SendEvent(Consts.C_Rotation90);
                }
                break;
            case MainGameStatus.Play:
                {
                    
                }
                break;
            case MainGameStatus.End:
                {

                }
                break;
        }
    }
    public int GetCurSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
    public void MainStatusSwitch(MainGameStatus mgs)
    {
        mainStatus = mgs;
        switch(mgs)
        {
            case MainGameStatus.StartUp:
                {
                    //进入新场景
                    print("主状态切换--准备进入新场景");
                    LoadScene(1);
                }
                break;
            case MainGameStatus.Logo:
                {
                    LoadScene(2);
                }
                break;
            case MainGameStatus.Menu:
                {
                    LoadScene(3);
                }
                break;
            case MainGameStatus.Play:
                {
                    //这边不用切换场景，发送一个下一步的命令就行
                    //SendEvent();
                }
                break;
            case MainGameStatus.End:
                {
                    LoadScene(4);
                }
                break;
        }
    }
}
