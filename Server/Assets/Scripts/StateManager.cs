using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Networking.Transport;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public enum UIStates
{
    login,
    create,
    lobby,
    room,
    game
}


public class StateManager : MonoBehaviour
{
    public List<Gameroom> gameRoomList;
    public List<Player> PlayersList;
    List<BaseState> stateList = new List<BaseState>();

    NetworkServer Server;
    BaseState CurrentState;
    loginState StateLogin = new loginState();
    lobbyState StateLobby= new lobbyState();
    createState LoginCrate = new createState();
    GameroomState StateGameroom = new GameroomState();
    GameplayState StateGameplay = new GameplayState();


    private void Start()
    {
        gameRoomList = new List<Gameroom>();
        PlayersList = new List<Player>();
        Server = GetComponent<NetworkServer>();

        stateList.Add(StateLogin);
        stateList.Add(StateLobby);
        stateList.Add(LoginCrate);
        stateList.Add(StateGameroom);
        stateList.Add(StateGameplay);

        foreach (BaseState state in stateList)
        {
            state.Start();
        }

            CurrentState = stateList[0];
    }

    public void MessageRecieved(string[] Information )
    {
        Player player = new Player();
        string NewState = Information[0];
        player.Username = Information[1];
        string Input2 = Information[2];

        int type;
        int.TryParse(Information[3], out type);
        int.TryParse(Information[4], out player.ConnectionID);
        foreach(BaseState state in stateList)
        {
            if (state.EnumState.ToString() == NewState)
            {
                CurrentState = state;
                break;
            }
        }
        switch(type)
        {
            case 0:
                CurrentState.OnContinueMessage(this, player, Input2);
                break;
            default:
                CurrentState.OnReturnMessage(this, player, Input2);
                break;

        }
    }
    

    public void SendMessageToClient(string message, int ID)
    {
        foreach (NetworkConnection connection in Server.GetNetworkConnections())
        {
            if(connection.InternalId == ID)
            {
                Server.SendMessageToClient(message, connection);
            }
        }


    }
    public void Update()
    {
    }
    // Start is called before the first frame update


}
