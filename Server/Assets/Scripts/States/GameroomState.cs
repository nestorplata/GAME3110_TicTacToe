using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Networking.Transport;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.Scripting.APIUpdating;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GameroomState : BaseState
{
    public override void OnRecievedMessage(StateManager manager,
            int signifier, int ID, string[] msg)
    {
        List<Player> CurrentRoomPlayerList = manager.ReturnGameroom(msg[0]).PlayersList;

        switch (signifier)
        {
            case ClientMessageType.OnContinue:

                switch (CurrentRoomPlayerList.Count)
                {
                    case 1:
                        message = "Waiting for new player";
                        manager.SendMessageToClient(Response(), ID);
                        break;
                    case 2:
                        message = "Moved To GamePlay As Player";
                        type = ServerToClientSignifiers.ContineSuccess;
                        CurrentRoomPlayerList[0].type = PlayerSignifiers.XPlayer;
                        CurrentRoomPlayerList[1].type = PlayerSignifiers.OPlayer;

                        foreach (Player player in CurrentRoomPlayerList)
                        {
                            manager.SendMessageToClient(type.ToString() + "," + message, player.ConnectionID);
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
                for (int i = 0; i < CurrentRoomPlayerList.Count; i++)
                {
                    if (CurrentRoomPlayerList[i].ConnectionID ==ID)
                    {
                        CurrentRoomPlayerList.RemoveAt(i);
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
