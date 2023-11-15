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
                message = "Moved to Gameplay";

                switch (manager.ReturnGameroom(msg[0]).PlayersList.Count)
                {
                    case 1:
                        Response(ServerToClientSignifiers.BasicFailure);
                        manager.SendMessageToClient(message, ID);
                        break;
                    case 2:
                        Response(ServerToClientSignifiers.BasicSuccess);
                        foreach (Player players in manager.ReturnGameroom(msg[0]).PlayersList)
                        {
                            manager.SendMessageToClient(message, players.ConnectionID);
                        }
                        break;
                    default:
                        Response(ServerToClientSignifiers.SuccessA);
                        manager.SendMessageToClient(message, ID);
                        break;

                }
                break;

            case ClientMessageType.OnReturn:
                for (int i = 0; i < manager.ReturnGameroom(ID).PlayersList.Count; i++)
                {
                    if (manager.ReturnGameroom(ID).PlayersList[i].ConnectionID ==ID)
                    {
                        manager.ReturnGameroom(ID).PlayersList.RemoveAt(i);
                        Response(ServerToClientSignifiers.ReturnSuccess);
                        break;
                    }
                }

                manager.SendMessageToClient(message, ID);

                break;

        }

    }

}
