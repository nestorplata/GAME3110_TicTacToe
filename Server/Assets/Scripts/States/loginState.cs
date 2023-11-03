using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class loginState : BaseState
{


    public override void Start()
    {
        EnumState = UIStates.login;
    }

    public override void OnContinueMessage(StateManager manager,
        Player player, string Input2)
    {
        string pathfile = "Accounts\\" + player.Username + ".txt";
        if (File.Exists(pathfile))
        {
            using (StreamReader sr = new StreamReader(pathfile))
            {
                if (Input2 == sr.ReadLine())
                {
                    manager.PlayersList.Add(player);
                    message = "Login Succeded";
                    successConfirmation();
                }
                else
                {
                    message = "Wrong Password";
                }
            }
        }
        else
        {
            message = "Wrong Username";

        }
        manager.SendMessageToClient(message, player.ConnectionID);

    }

    public override void OnReturnMessage(StateManager manager,
        Player player, string Input2)
    {

    }


}
