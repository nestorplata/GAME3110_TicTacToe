using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : BaseState
{

    public override void Start()
    {
        EnumState = UIStates.game;
    }

    public override void OnContinueMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {

    }

    public override void OnReturnMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {

    }

}
