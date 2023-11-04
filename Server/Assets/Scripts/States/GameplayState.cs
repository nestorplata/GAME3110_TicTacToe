using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : BaseState
{

    public override void Start()
    {
        EnumState = UIStates.game;
    }

    public override void OnContinueMessage(StateManager manager,
        Player player, string Input2)
    {
        foreach (Gameroom room in manager.gameRoomList)
        {
            if(room.isOnRoom(player))
            {
                foreach (Player n_player in room.PlayersList)
                {
                    if (n_player.Username != player.Username)
                    {
                        manager.SendMessageToClient(Input2, n_player.ConnectionID);
                    }
                }
                break;
            }

            
            
        }

    }

    public override void OnReturnMessage(StateManager manager,
        Player player, string Input2)
    {
        message = "Unable to be removed from Gameplay";

        foreach (Gameroom room in manager.gameRoomList)
        {
            if (room.isOnRoom(player))
            {
                for (int i = 0; i < room.PlayersList.Count; i++)
                {
                    if (room.PlayersList[i].ConnectionID == player.ConnectionID)
                    {
                        room.PlayersList.RemoveAt(i);
                        message = "Removed from Gameplay";
                        break;
                    }
                }
                break;
            }
        }

        manager.SendMessageToClient(message, player.ConnectionID);

    }

}
