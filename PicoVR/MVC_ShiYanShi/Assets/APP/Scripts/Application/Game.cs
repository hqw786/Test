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

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //pool = ObjectPool.Instance;
        sound = Sound.Instance;
        //configData = ConfigData.Instance;
        RegisterController(Consts.C_StartUp, new StartUpCommand());
        SendEvent(Consts.C_StartUp);
    }
    public void LoadScene(int level)
    {
        SceneArgs e = new SceneArgs();
        e.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SendEvent(Consts.C_ExitScene, e);
        SceneManager.LoadScene(level);
    }
    void OnLevelWasLoaded(int level)
    {
        SceneArgs e = new SceneArgs();
        e.sceneIndex = level;
        SendEvent(Consts.C_EnterScene, e);
    }
}
