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
        if (File.Exists("Accounts\\" + msg[0] + ".txt"))
        {
            using (StreamReader sr = new StreamReader("Accounts\\" + msg[0] + ".txt"))
            {
                if (msg[1] == sr.ReadLine())
                {
                    if (manager.ReturnPlayer(msg[0])==null)
                    {
                        manager.ReturnPlayer(ID).Username = msg[0];
                        Response(ServerToClientSignifiers.BasicSuccess);
                    }
                    else
                    {
                        Response(ServerToClientSignifiers.FailureB);
                    }
                }
                else
                {
                    Response(ServerToClientSignifiers.FailureA);
                }
            }
        }
        else
        {
            Response(ServerToClientSignifiers.BasicFailure);
        }
        manager.SendMessageToClient(message, ID);
    }

}
