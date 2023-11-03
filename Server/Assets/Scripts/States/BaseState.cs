using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState
{
    public UIStates EnumState;
    public string message = "none";

    public abstract void Start();
    public abstract void OnContinueMessage(StateManager manager, 
        Player player, string Input2);
    public abstract void OnReturnMessage(StateManager manager, 
        Player player, string Input2);
 
    public void successConfirmation()
    {
        message = "success," + message;

    }


}
