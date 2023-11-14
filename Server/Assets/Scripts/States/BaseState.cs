using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public string message = "none";

    public abstract void OnRecievedMessage(StateManager manager, 
        int signifier, int ID, string[] msg);

    public void Response(int type, string additional = "")
    {
        if(additional!="")
        {
            additional = ',' + additional;
        }
        message = type.ToString()+ additional;

    }


}
