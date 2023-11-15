using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lobbyState : BaseState
{

    public override void OnRecievedMessage(StateManager manager,
    int signifier, int ID, string[] msg)
    {
        switch (signifier)
        {
            case ClientMessageType.OnContinue:

                if (manager.ReturnGameroom(msg[0])!=null)
                {
                    manager.ReturnGameroom(msg[0]).PlayersList.Add(manager.ReturnPlayer(ID));
                    message = "Gameroom Joined";
                }
                else
                {
                    Gameroom room = new Gameroom();
                    room.PlayersList.Add(manager.ReturnPlayer(ID));
                    room.GameroomID = msg[0];
                    manager.GameroomList.Add(room);
                    message = "Gameroom Created";

                }
                type = ServerToClientSignifiers.ContineSuccess;
                break;

            case ClientMessageType.OnReturn:
                manager.ReturnPlayer(ID).Username = "";
                message = "Logged Off";
                type = ServerToClientSignifiers.ReturnSuccess;
                break;

        }

        manager.SendMessageToClient(Response(), ID);
    }
}
