using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEditor.MemoryProfiler;
using UnityEngine;

public class loginState : BaseState
{
    public override void OnRecievedMessage(StateManager manager,
        int signifier, int ID, string[] msg)
    {
        Player CurrentPlayer = manager.ReturnPlayer(msg[0]);

        message = "wrong Username";

        if (File.Exists("Accounts\\" + msg[0] + ".txt"))
        {
            using (StreamReader sr = new StreamReader("Accounts\\" + msg[0] + ".txt"))
            {
                if (msg[1] == sr.ReadLine())
                {
                    if (CurrentPlayer == null)
                    {
                        CurrentPlayer.Username = msg[0];
                        type = ServerToClientSignifiers.ContineSuccess;
                        message = "logged in";
                    }
                    else
                    {
                        message = "Account Already in use";
                    }
                }
                else
                {
                    message = "Wrong Password";
                }
            }
        }

        manager.SendMessageToClient(Response(), ID);
    }

}
