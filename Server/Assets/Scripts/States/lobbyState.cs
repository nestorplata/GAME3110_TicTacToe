using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobbyState : BaseState
{

    public override void Start()
    {
        EnumState = UIStates.lobby;
    }

    public override void OnContinueMessage(StateManager manager,
        Player player, string Input2)
    {
        if (manager.gameRoomList.Count < 1)
        {
            Gameroom room = new Gameroom();
            room.PlayersList.Add(player);
            room.GameroomID = Input2;
            manager.gameRoomList.Add(room);
            message = "GameRoom Created";

        }
        else
        {
            foreach (Gameroom room in manager.gameRoomList)
            {
                if (room.GameroomID == Input2)
                {

                    room.PlayersList.Add(player);
                    message = "GameRoom Joined";
                    break;
                }
            }
        }


        successConfirmation();
        manager.SendMessageToClient(message, player.ConnectionID);
    }

    public override void OnReturnMessage(StateManager manager,
        Player player, string Input2)
    {
        message = "unable to log off";
        for (int i = 0; i < manager.PlayersList.Count; i++)
        {
            if (manager.PlayersList[i].ConnectionID == player.ConnectionID)
            {
                manager.PlayersList.RemoveAt(i);
                message = "Logged Off";
                break;
            }
        }   
        manager.SendMessageToClient(message, player.ConnectionID);

    }

}
