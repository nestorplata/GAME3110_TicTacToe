using System;
using System.Collections;
using System.Collections.Generic;
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

    public int MessageRecieved(string[] Information )
    {
        string NewState = Information[0];
        string Input1 = Information[1];
        string Input2 = Information[2];

        int type;
        int ConnectionID;
        int.TryParse(Information[3], out type);
        int.TryParse(Information[4], out ConnectionID);
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
                CurrentState.OnContinueMessage(this, Input1, Input2,  ConnectionID);
                break;
            case 1:
                CurrentState.OnReturnMessage(this, Input1, Input2, ConnectionID);
                break;

        }
        return ConnectionID;
    }
    public string GetMessage()
    {
        string message = CurrentState.message;
        if (CurrentState.IsSuccesfull)
        {
            message = "success,"+ CurrentState.message;
            CurrentState.IsSuccesfull = false;
        }
        return message;

    }
    public void Update()
    {
    }
    // Start is called before the first frame update


}
