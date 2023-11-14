using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : BaseState
{
    public override void OnRecievedMessage(StateManager manager,
        int signifier, int ID, string[] msg)
    {
        switch (signifier)
        {
            case ClientMessageType.OnContinue:
                foreach (Player n_player in manager.ReturnGameroom(ID).PlayersList)
                {
                    if (n_player.ConnectionID != ID)
                    {
                        message = manager.ReturnPlayer(ID).Username + " Sent this: \"" + msg[0] + "\"";
                        Response(ServerToClientSignifiers.SuccessA, message);
                        manager.SendMessageToClient(message, n_player.ConnectionID);
                    }
                }
                break;

            case ClientMessageType.OnReturn:
                for (int i = 0; i < manager.ReturnGameroom(ID).PlayersList.Count; i++)
                {
                    if (manager.ReturnGameroom(ID).PlayersList[i].ConnectionID == ID)
                    {
                        manager.ReturnGameroom(ID).PlayersList.RemoveAt(i);
                        Response(ServerToClientSignifiers.SuccessA);
                        break;
                    }
                }
                manager.SendMessageToClient(message,ID);
                break;

            case ClientMessageType.OnSpecial:
                Response(ServerToClientSignifiers.BasicSuccess, message);
                manager.SendMessageToClient(message, ID);
                break;

        }

    }

}
