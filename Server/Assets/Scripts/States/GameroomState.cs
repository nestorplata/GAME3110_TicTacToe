using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.Scripting.APIUpdating;

public class GameroomState : BaseState
{

    public override void Start()
    {
        EnumState = UIStates.room;
        message = "waiting for new player";
    }

    public override void OnContinueMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {
        foreach (Gameroom room in manager.gameRoomList)
        {

            if (room.PlayersList.Count > 1 && room.GameroomID == Input2)
            {
                message = "Moved to GamePlay";
                IsSuccesfull = true;

                if (room.PlayersList.Count == 2)
                {
                    message = message + "As Player";
                }
                else
                {
                    message = "Observer " + message;

                }
            }

        }
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
                message = "Removed from GameRoom ";
                IsSuccesfull = true;

                break;
            }
        }
    }

}
