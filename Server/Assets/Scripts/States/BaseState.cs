using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public string message = "none";
    public int type = ServerToClientSignifiers.Failure;

    public abstract void OnRecievedMessage(StateManager manager, 
        int signifier, int ID, string[] msg);

    public string Response()
    {

        message = type.ToString()+ "," +message;
        //Remove to quikly acces GamePlay
        type = ServerToClientSignifiers.Failure;
        return message;
    }


}
