using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayState : BaseState
{

    public override void Start()
    {
        EnumState = UIStates.game;
    }

    public override void OnContinueMessage(StateManager manager,
        Player player, string Input2)
    {
        message = Input2;
        manager.SendMessageToClient(message, player.ConnectionID);

    }

    public override void OnReturnMessage(StateManager manager,
        Player player, string Input2)
    {
        foreach (Gameroom room in manager.gameRoomList)
        {
            if (room.PlayersList.Contains(player)) 
            {
                room.PlayersList.Remove(player);
                message = "Removed from Gameplay and Gameroom";
                //successConfirmation();
                manager.SendMessageToClient(message, player.ConnectionID);
                message = "none";

                break;
            }
        }
    }

}
