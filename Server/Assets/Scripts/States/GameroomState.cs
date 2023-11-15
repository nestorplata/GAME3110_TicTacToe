using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.Scripting.APIUpdating;

public class GameroomState : BaseState
{
    public override void OnRecievedMessage(StateManager manager,
            int signifier, int ID, string[] msg)
    {
        switch (signifier)
        {
            case ClientMessageType.OnContinue:

                switch (manager.ReturnGameroom(msg[0]).PlayersList.Count)
                {
                    case 1:
                        message = "Waiting for new player";
                        manager.SendMessageToClient(Response(), ID);
                        break;
                    case 2:
                        message = "Moved To GamePlay As Player";
                        type = ServerToClientSignifiers.ContineSuccess;
                        foreach (Player player in manager.ReturnGameroom(msg[0]).PlayersList)
                        {
                            manager.SendMessageToClient(Response(), player.ConnectionID);
                        }
                        break;
                    default:
                        message = "Moved To GamePlay As Observer";
                        type = ServerToClientSignifiers.ContinueAsObserver;

                        manager.SendMessageToClient(Response(), ID);
                        break;
                }
                break;

            case ClientMessageType.OnReturn:
                message = "Unable To Remove from Gameroom";
                for (int i = 0; i < manager.ReturnGameroom(ID).PlayersList.Count; i++)
                {
                    if (manager.ReturnGameroom(ID).PlayersList[i].ConnectionID ==ID)
                    {
                        manager.ReturnGameroom(ID).PlayersList.RemoveAt(i);
                        type = ServerToClientSignifiers.ReturnSuccess;
                        message = "Removed from Gameooom";
                        break;
                    }
                }

                manager.SendMessageToClient(Response(), ID);

                break;

        }

    }

}
