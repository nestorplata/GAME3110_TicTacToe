using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobbyState : BaseState
{

    public override void Start()
    {
        EnumState = UIStates.lobby;
    }

    public override void OnContinueMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {
        bool IsRoomJoined = false;

        foreach (Gameroom room in manager.gameRoomList)
        {
            if (room.GameroomID == Input2)
            {
                room.PlayersList.Add(manager.PlayersList[0]);
                IsRoomJoined = true;
                message = "GameRoom Joined";

                break;
            }
        }
        if (!IsRoomJoined)
        {
            Gameroom room = new Gameroom();
            room.PlayersList.Add(manager.PlayersList[0]);
            room.GameroomID = Input2;
            manager.gameRoomList.Add(room);
            message = "GameRoom Created";

        }
        IsSuccesfull = true;


    }

    public override void OnReturnMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {
        foreach (Gameroom room in manager.gameRoomList)
        {
            if (room.GameroomID == Input2)
            {
                Player player = new Player();
                player.Username = Input1;
                player.ConnectionID= ConnectionID;
                room.PlayersList.Remove(player);
                message = "Logged Off";
                IsSuccesfull = true;

                break;
            }
        }
    }

}
