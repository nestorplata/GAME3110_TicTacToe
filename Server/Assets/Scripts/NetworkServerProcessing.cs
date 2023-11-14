using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class NetworkServerProcessing
{


    #region Send and Receive Data Functions
    static public void ReceivedMessageFromClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        Debug.Log("Network msg received =  " + msg + ", from connection id = " + clientConnectionID + ", from pipeline = " + pipeline);

        string[] csv = msg.Split(',');
        int CTVsignifier = int.Parse(csv[0]);
        int type = int.Parse(csv[1]);
        string[] data = csv;
        if (csv.Length>2)
        {
            data= csv[2].Split('_');
        }
        stateManager.MessageRecieved(CTVsignifier, type, clientConnectionID, data);



        //gameLogic.DoSomething();
    }
    static public void SendMessageToClient(string msg, int clientConnectionID, TransportPipeline pipeline)
    {
        networkServer.SendMessageToClient(msg, clientConnectionID, pipeline);
    }

    #endregion

    #region Connection Events

    static public void ConnectionEvent(int clientConnectionID)
    {
        Debug.Log("Client connection, ID == " + clientConnectionID);
        Player player = new Player();
        player.ConnectionID = clientConnectionID;
        stateManager.PlayersList.Add(player);
    }
    static public void DisconnectionEvent(int clientConnectionID)
    {
        Debug.Log("Client disconnection, ID == " + clientConnectionID);
        for (int i = 0; i < stateManager.PlayersList.Count; i++)
        {
            if (stateManager.PlayersList[i].ConnectionID==clientConnectionID)
            {
                stateManager.PlayersList.RemoveAt(i);
            }
        }
    }

    #endregion

    #region Setup
    static NetworkServer networkServer;
    static StateManager stateManager;

    static public void SetNetworkServer(NetworkServer NetworkServer)
    {
        networkServer = NetworkServer;
    }
    static public void SetStateManager(StateManager manager)
    {
        stateManager = manager;
    }

    static public NetworkServer GetNetworkServer()
    {
        return networkServer;
    }


    #endregion
}





