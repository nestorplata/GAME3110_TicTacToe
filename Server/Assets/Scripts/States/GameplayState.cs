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
        message = Input2;
    }

    public override void OnReturnMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {
        foreach (Gameroom room in manager.gameRoomList)
        {
            if (room.GameroomID == Input2)
            {
                Player player = new Player();
                player.ConnectionID = ConnectionID;
                player.Username = Input1;
                room.PlayersList.Remove(player);
                message = "Removed from Gameplay and Gameroom";
                IsSuccesfull = true;

                break;
            }
        }
    }

}
