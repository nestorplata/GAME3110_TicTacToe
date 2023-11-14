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
                    Response(ServerToClientSignifiers.SuccessA);

                }
                else
                {
                    Gameroom room = new Gameroom();
                    room.PlayersList.Add(manager.ReturnPlayer(ID));
                    room.GameroomID = msg[0];
                    manager.GameroomList.Add(room);
                    Response(ServerToClientSignifiers.BasicSuccess);

                }
                break;

            case ClientMessageType.OnReturn:
                manager.ReturnPlayer(ID).Username = "";
                Response(ServerToClientSignifiers.ReturnSuccess);
                break;

        }

        manager.SendMessageToClient(message, ID);

    }
}
