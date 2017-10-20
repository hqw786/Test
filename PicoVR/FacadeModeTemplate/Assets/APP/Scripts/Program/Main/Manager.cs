using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class Manager : SingelationT<Manager>
{
    #region 单例
    //private static Manager instance;
    //public static Manager Instance
    //{
    //    get
    //    {
    //        if (instance == null) instance = new Manager();
    //        return instance;
    //    }
    //}
    public Manager()
    {
        statusMachine = new StatusMachine();
    }
    #endregion

    //状态机引用
    public StatusMachine statusMachine;


    internal void Init()
    {
        Facade.Instance.Init();
        Facade.Instance.HandleMessage(Consts.Msg_UI_Score, null);
        

        statusMachine.SetCurAppStatus(APPStatus.Run);
    }
}
