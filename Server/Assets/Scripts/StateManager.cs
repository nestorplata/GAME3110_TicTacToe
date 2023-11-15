using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEditor.MemoryProfiler;
using UnityEditor.VersionControl;
using UnityEngine;




public class StateManager : MonoBehaviour
{
    public List<Gameroom> GameroomList = new List<Gameroom>();
    public List<Player> PlayersList = new List<Player>();

    loginState StateLogin = new loginState();
    createState StateCreate = new createState();
    lobbyState StateLobby = new lobbyState();
    GameroomState StateGameroom = new GameroomState();
    GameplayState StateGameplay = new GameplayState();

    private void Start()
    {
        NetworkServerProcessing.SetStateManager(this);
    }

    public void MessageRecieved(int CTSsignifier, int type, int ID, string[] msg )
    {
        switch (CTSsignifier)
        {
            case ClientToServerSignifiers.login:
                StateLogin.OnRecievedMessage(this, type, ID, msg);
                break;
            case ClientToServerSignifiers.create:
                StateCreate.OnRecievedMessage(this, type, ID, msg);
                break;
            case ClientToServerSignifiers.lobby:
                StateLobby.OnRecievedMessage(this, type, ID, msg);
                break;
            case ClientToServerSignifiers.room:
                StateGameroom.OnRecievedMessage(this, type, ID, msg);
                break;
            case ClientToServerSignifiers.game:
                StateGameplay.OnRecievedMessage(this, type, ID, msg);
                break;
            default:
                Debug.Log("Unable to Process state");
                break;
        }
    }


    public void SendMessageToClient(string message, int ID)
    {
        NetworkServerProcessing.SendMessageToClient(message, ID, TransportPipeline.ReliableAndInOrder);
    }
    
    public Player ReturnPlayer(int ID)
    { 
        foreach (Player player in PlayersList) 
        {
            if(player.ConnectionID==ID)
            {
                return player;
            }
        }
        return null;
    }

    public Player ReturnPlayer(string Username)
    {
        foreach (Player player in PlayersList)
        {
            if (player.Username == Username)
            {
                return player;
            }
        }
        return null;
    }

    public Gameroom ReturnGameroom(string ID)
    {
        foreach (Gameroom room in GameroomList)
        {
            if (room.GameroomID == ID)
            {
                return room;
            }
        }
        return null;
    }

    public Gameroom ReturnGameroom(int ID)
    {
        foreach (Gameroom room in GameroomList)
        {
            foreach (Player player in room.PlayersList)
            {
                if (player.ConnectionID == ID)
                {
                    return room;
                }
            }
        }
        return null;

    }

}

#region Protocol Signifiers
static public class ClientToServerSignifiers
{
    public const int none = 0;
    public const int login = 1;
    public const int create = 2;
    public const int lobby = 3;
    public const int room = 4;
    public const int game = 5;
}

static public class ClientMessageType
{
    public const int OnContinue = 0;
    public const int OnReturn = 1;
    public const int OnSpecial = 2;
}

static public class ServerToClientSignifiers
{
    public const int Failure = -1;
    public const int ContineSuccess = 0;
    public const int ContinueAsObserver = 1;
    public const int ReturnSuccess = 2;
    public const int SpecialSuccess = 3;
    public const int EnemyMoved = 4;
    public const int GAMEEND= 5;

}

#endregion
