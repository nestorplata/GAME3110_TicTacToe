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
        //manager.gameRoomList.Find(GameroomID)
        foreach (Gameroom room in manager.gameRoomList)
        {
            if (room.GameroomID == Input2)
            {
                
                room.PlayersList.Add(player);
                message = "GameRoom Joined";
                break;
            }
        }
        if (manager.gameRoomList.Count<1)
        {
            Gameroom room = new Gameroom();
            room.PlayersList.Add(player);
            room.GameroomID = Input2;
            manager.gameRoomList.Add(room);
            message = "GameRoom Created";

        }
        successConfirmation();

        manager.SendMessageToClient(message, player.ConnectionID);
        message = "none";
    }

    public override void OnReturnMessage(StateManager manager,
        Player player, string Input2)
    {
        foreach (Gameroom room in manager.gameRoomList)
        {
            if (room.GameroomID == Input2)
            {
                room.PlayersList.Remove(player);
                message = "Logged Off";
                //successConfirmation();
                manager.SendMessageToClient(message, player.ConnectionID);
                message = "none";

                break;
            }
        }
    }

}
