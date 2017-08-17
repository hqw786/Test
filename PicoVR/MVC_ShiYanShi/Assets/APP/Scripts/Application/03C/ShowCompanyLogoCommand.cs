﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ShowCompanyLogoCommand : Controller, ICommand
{

    public void Execute(object data)
    {
        //注册视图
        RegisterView(GameObject.Find("/Canvas/LogoPanel").GetComponent<ShowCompanyLogoView>());
        ShowCompanyLogoView sclv = GetView<ShowCompanyLogoView>();
        sclv.RegisterEvents();
    }
}
