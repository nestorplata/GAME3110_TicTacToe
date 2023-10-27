using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class loginState : BaseState
{


    public override void Start()
    {
        EnumState = UIStates.login;
    }

    public override void OnContinueMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {
        string pathfile = "Accounts\\" + Input1 + ".txt";
        if (File.Exists(pathfile))
        {
            using (StreamReader sr = new StreamReader(pathfile))
            {
                if (Input2 == sr.ReadLine())
                {
                    message = "Login Succeded";
                    IsSuccesfull = true;
                    Player player = new Player();
                    player.Username = Input1;
                    player.ConnectionID = ConnectionID;
                    manager.PlayersList.Add(player);
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
    }

    public override void OnReturnMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {

    }


}
