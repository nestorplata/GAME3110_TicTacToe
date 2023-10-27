using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public UIStates EnumState;
    public string message = "none";
    public bool IsSuccesfull;

    public abstract void Start();
    public abstract void OnContinueMessage(StateManager manager, string Input1, 
        string Input2,int ConnectionID);
    public abstract void OnReturnMessage(StateManager manager, string Input1,
        string Input2,  int ConnectionID);
 



}
