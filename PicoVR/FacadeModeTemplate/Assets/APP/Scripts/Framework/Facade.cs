using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//完成内部
public class Facade
{
    
    private static Facade instance;
    public static Facade Instance
    {
        get
        {
            if (instance == null) instance = new Facade();
            return instance;
        }
    }

    Pool pool;
    Sound sound;
    UIManager uimanager;
	// Use this for initialization
    private Facade() 
    {
        pool = Pool.Instance;
        sound = Sound.Instance;
        uimanager = UIManager.Instance;
    }
    //内部初始化
    public void Init ()
    {
        #region 测试 实例化预置体并移动
        pool.AddPool(Consts.Pool_monster1);
        for (int i = 0; i < 1; i++)
        {
            GameObject g = Tools.InstantiateGameObject(Consts.Pool_monster1,
                Vector3.zero + new Vector3(1f * i, 1f, 0f),
                Vector3.one, Quaternion.identity);
            g.transform.DOMove(Vector3.forward * 50f, 5f).SetLoops(-1 , LoopType.Yoyo);
            g.transform.DOScale(Vector3.one * 1.5f, 5f).SetLoops(-1, LoopType.Yoyo);
        }

        pool.AddPool(Consts.Pool_monster2);
        for (int i = 0; i < 1; i++)
        {
            GameObject g = Tools.InstantiateGameObject(Consts.Pool_monster2,
                Vector3.zero + new Vector3(1f * i, 1f, 0f),
                Vector3.one, Quaternion.identity);
            g.transform.DOMove(-Vector3.forward * 50f, 5f).SetLoops(-1, LoopType.Yoyo);
            g.transform.DOScale(Vector3.one * 1.5f, 5f).SetLoops(-1, LoopType.Yoyo);
        }
        #endregion
    }
	public void HandleMessage(string message)
    {
        if(message.Contains("Msg_UI_"))
        {
            uimanager.HandleMessage(message);
        }

        //对象池不用事件，这里改成其他功能的消息
        if(message.Contains("Msg_Pool_"))
        {
            //
        }
    }
	
}
