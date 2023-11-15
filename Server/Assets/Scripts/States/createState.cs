using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class createState : BaseState
{

    public override void OnRecievedMessage(StateManager manager,
    int signifier, int ID, string[] msg)
    {
        Player CurrentPlayer = manager.ReturnPlayer(msg[0]);

        message = "Account Already Exists";

        if (!File.Exists("Accounts\\" + msg[0] + ".txt"))
        {
            using (StreamWriter sw = new StreamWriter("Accounts\\" + msg[0] + ".txt"))
            {
                sw.WriteLine(msg[1]);
            }
            CurrentPlayer.Username = msg[0];
            message = "Account Created";
            type = ServerToClientSignifiers.ContineSuccess;

        }

        manager.SendMessageToClient(Response(), ID);
    }


}
