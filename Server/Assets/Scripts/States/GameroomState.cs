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
    }

    public override void OnContinueMessage(StateManager manager,
        Player player, string Input2)
    {

        foreach (Gameroom room in manager.gameRoomList)
        {
            if(room.GameroomID == Input2)   
            {
                message = "Moved to GamePlay";

                switch (room.PlayersList.Count)
                {
                    case 1:
                        message = "waiting for new player";
                        manager.SendMessageToClient(message, player.ConnectionID);
                        break;
                    case 2:
                        successConfirmation();
                        message = message + " As Player";
                        foreach (Player players in room.PlayersList)
                        {
                            manager.SendMessageToClient(message, players.ConnectionID);

                        }
                        break;
                    default :
                        successConfirmation();
                        message = "Observer " + message;
                        manager.SendMessageToClient(message, player.ConnectionID);
                        break;

                }
                break;
            }


        }
    }

    public override void OnReturnMessage(StateManager manager,
        Player player, string Input2)
    {
        foreach (Gameroom room in manager.gameRoomList)
        {
            if (room.GameroomID == Input2)
            {
                room.PlayersList.Remove(player);
                message = "Removed from GameRoom ";
                //successConfirmation();
                manager.SendMessageToClient(message, player.ConnectionID);
                message = "waiting for new player";

                break;
            }
        }
    }

}
