using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class createState : BaseState
{
    public override void Start()
    {
        EnumState = UIStates.create;
    }

    public override void OnContinueMessage(StateManager manager,
        Player player, string Input2)
    {
        string pathfile = "Accounts\\" + player.Username + ".txt";

        if (!File.Exists(pathfile))
        {
            using (StreamWriter sw = new StreamWriter(pathfile))
            {
                sw.WriteLine(Input2);
            }
            manager.PlayersList.Add(player);
            message = "Account Created";
            successConfirmation();
        }
        else
        {
            message = "Account Already Exists";
        }
        manager.SendMessageToClient(message, player.ConnectionID);
    }

    public override void OnReturnMessage(StateManager manager,
        Player player, string Input2)
    {

    }

}
