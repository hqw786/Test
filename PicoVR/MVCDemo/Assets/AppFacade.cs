using UnityEngine;
using System.Collections;

public class AppFacade : MonoBehaviour
{

    public static AppFacade Intance;
    Controller controller;
    View view;

    public void Awake()
    {
        Intance = this;
        controller = new Controller();
        view = new View();
    }


    // Use this for initialization
    void Start()
    {
        RenderToViewCommand render = new RenderToViewCommand();
        //render.view = PackView.Intance;
        this.RestierCommand("RenderToViewCommand", render);
        this.RestierCommand("AddGoodCommand", new AddGoodCommand());
        this.ResierView(new PackView());
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //this.Excute("RenderToViewCommand");
            this.Excute(new INotifier("RenderToViewCommand"));
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //this.Excute("AddGoodCommand");
            this.Excute(new INotifier("AddGoodCommand",1));
        }
    }

    public void ResierView(Mediator mediator)
    {
        if (mediator!=null)
        {
            view.ResiterView(mediator);
        }
    }

    public void ExcuteToView(INotifier notifier)
    {
        if (notifier!=null)
        {
            view.Excute(notifier); 
        }
    }

    public void RestierCommand(string msg,ICommand command)
    {
        this.controller.ResiterCommand(msg,command);
    }

    public void Excute(INotifier inotifier)
    {
        this.controller.ExcuteCommand(inotifier);
    }
    
}
