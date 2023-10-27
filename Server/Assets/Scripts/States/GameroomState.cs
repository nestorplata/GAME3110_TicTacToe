using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameroomState : BaseState
{

    public override void Start()
    {
        EnumState = UIStates.room;
    }

    public override void OnContinueMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {

    }

    public override void OnReturnMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {
        foreach (Gameroom room in manager.gameRoomList)
        {
            if (room.GameroomID == Input2)
            {
                room.PlayersList.Remove(manager.PlayersList[0]);
                message = "Removed from GameRoom ";
                IsSuccesfull = true;

                break;
            }
        }
    }

}
