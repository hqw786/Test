using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum APPStatus
{
    Init, Run, End, Exit
}
public enum RUNStatus
{
    None, Start, Runing, End
}
public class StatusMachine
{
    APPStatus curAppStatus = APPStatus.Init;
    RUNStatus curRunStatus = RUNStatus.None;
    public void Run()
    {
        switch (curAppStatus)
        {
            case APPStatus.Init:
                {
                    Manager.Instance.Init();
                }
                break;
            case APPStatus.Run:
                {
                    switch(curRunStatus)
                    {
                        case RUNStatus.None:
                            {

                            }
                            break;
                        case RUNStatus.Start:
                            {

                            }
                            break;
                        case RUNStatus.Runing:
                            {

                            }
                            break;
                        case RUNStatus.End:
                            {

                            }
                            break;
                    }
                }
                break;
            case APPStatus.End:
                {

                }
                break;
            case APPStatus.Exit:
                {

                }
                break;
        }
    }
    public void SetCurAppStatus(APPStatus status)
    {
        curAppStatus = status;
    }
    public APPStatus GetCurAppStatus()
    {
        return curAppStatus;
    }
    public void SetCurRunStatus(RUNStatus status)
    {
        curRunStatus = status;
    }
    public RUNStatus GetCurRunStatus()
    {
        return curRunStatus;
    }
}
