using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobbyState : BaseState
{

    public override void OnRecievedMessage(StateManager manager,
    int signifier, int ID, string[] msg)
    {
        Player CurrentPlayer = manager.ReturnPlayer(ID);

        switch (signifier)
        {
            case ClientMessageType.OnContinue:
                Gameroom CurrentRoom = manager.ReturnGameroom(msg[0]);

                if (CurrentRoom!=null)
                {
                    CurrentRoom.PlayersList.Add(CurrentPlayer);
                    message = "Gameroom Joined";
                }
                else
                {
                    Gameroom room = new Gameroom();
                    room.PlayersList.Add(CurrentPlayer);
                    room.GameroomID = msg[0];
                    manager.GameroomList.Add(room);
                    message = "Gameroom Created";

                }
                type = ServerToClientSignifiers.ContineSuccess;
                break;

            case ClientMessageType.OnReturn:
                CurrentPlayer.Username = "";
                message = "Logged Off";
                type = ServerToClientSignifiers.ReturnSuccess;
                break;

        }

        manager.SendMessageToClient(Response(), ID);
    }
}
