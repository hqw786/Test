using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CommandNotifierArgs
{
    public string msg;
    public object body;
    public string sender;

    public CommandNotifierArgs(string msg, object body, string sender)
    {
        this.sender = sender;
        this.body = body;
        this.msg = msg;
    }

    public CommandNotifierArgs(string msg, object body)
        : this(msg, body, string.Empty)
    {

    }

    public CommandNotifierArgs(string msg)
        : this(msg, null, string.Empty)
    {

    }


    public CommandNotifierArgs()
    {

    }
}

