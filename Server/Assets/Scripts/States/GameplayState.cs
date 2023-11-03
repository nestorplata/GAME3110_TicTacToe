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
            if(room.PlayersList.Contains(player))
            {
                foreach (Player n_player in room.PlayersList)
                {
                    
                    if(n_player.ConnectionID !=player.ConnectionID)
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
            if (room.GameroomID == Input2)
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

                manager.SendMessageToClient(message, player.ConnectionID);

                break;
            }
        }
    }

}
