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

    public override void OnContinueMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {
        string pathfile = "Accounts\\" + Input1 + ".txt";

        if (!File.Exists(pathfile))
        {
            using (StreamWriter sw = new StreamWriter(pathfile))
            {
                sw.WriteLine(Input2);
            }
            message = "Account Created";
            IsSuccesfull = true;


        }
        else
        {
            message = "Account Already Exists";
        }
    }

    public override void OnReturnMessage(StateManager manager, string Input1,
        string Input2, int ConnectionID)
    {

    }

}
