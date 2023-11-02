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
            message = "Account Created";
            successConfirmation();
            manager.PlayersList.Add(player);


        }
        else
        {
            message = "Account Already Exists";
        }
        manager.SendMessageToClient(message, player.ConnectionID);
        message = "none";
    }

    public override void OnReturnMessage(StateManager manager,
        Player player, string Input2)
    {

    }

}
