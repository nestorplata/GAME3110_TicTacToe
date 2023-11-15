using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class GameplayState : BaseState
{
    public override void OnRecievedMessage(StateManager manager,
        int signifier, int ID, string[] msg)
    {
        switch (signifier)
        {
            case ClientMessageType.OnContinue:
                foreach (Player n_player in manager.ReturnGameroom(ID).PlayersList)
                {
                    if (n_player.ConnectionID != ID)
                    {
                        message = manager.ReturnPlayer(ID).Username + " Sent this: \"" + msg[0] + "\"";
                        type = ServerToClientSignifiers.ContineSuccess;
                        manager.SendMessageToClient(Response(), n_player.ConnectionID);
                    }
                }
                break;

            case ClientMessageType.OnReturn:
                for (int i = 0; i < manager.ReturnGameroom(ID).PlayersList.Count; i++)
                {
                    if (manager.ReturnGameroom(ID).PlayersList[i].ConnectionID == ID)
                    {
                        message = "Removed from Gameplay";
                        manager.ReturnGameroom(ID).PlayersList.RemoveAt(i);
                        type = ServerToClientSignifiers.ReturnSuccess;
                        manager.SendMessageToClient(Response(), ID);
                        break;

                    }
                }
                break;

            case ClientMessageType.OnSpecial:
                message = "position is Ocupied by " + manager.ReturnGameroom(ID).grid[int.Parse(msg[0])];

                if (manager.ReturnGameroom(ID).grid[int.Parse(msg[0])] != 'X' &&
                    manager.ReturnGameroom(ID).grid[int.Parse(msg[0])] != 'O')
                {
                    if (manager.ReturnGameroom(ID).PlayersList[0].ConnectionID==ID)
                    {
                        manager.ReturnGameroom(ID).grid[int.Parse(msg[0])] = 'O';
                    }
                    else
                    {
                        manager.ReturnGameroom(ID).grid[int.Parse(msg[0])] = 'X';
                    }

                    //If end not reached
                    if (CheckWin(manager.ReturnGameroom(ID).grid) != 0)
                    {
                        message = "End Reached";
                        type = ServerToClientSignifiers.GAMEEND;

                    }
                    else
                    {
                        //send message to other Players
                        foreach (Player n_player in manager.ReturnGameroom(ID).PlayersList)
                        {
                            if (n_player.ConnectionID != ID)
                            {
                                message = "Oponent Played on: " + msg[0];
                                manager.SendMessageToClient(Response(), n_player.ConnectionID);
                            }
                        }
                        message = "Successfull play on: " + msg[0];
                        type = ServerToClientSignifiers.SpecialSuccess;

                    }
                }
                manager.SendMessageToClient(Response(), ID);

                break;

        }

    }

    private static int CheckWin(char[] arr)
    {
        #region Horzontal Winning Condtion
        //Winning Condition For First Row
        if (arr[1] == arr[2] && arr[2] == arr[3])
        {
            return 1;
        }
        //Winning Condition For Second Row
        else if (arr[4] == arr[5] && arr[5] == arr[6])
        {
            return 1;
        }
        //Winning Condition For Third Row
        else if (arr[6] == arr[7] && arr[7] == arr[8])
        {
            return 1;
        }
        #endregion
        #region vertical Winning Condtion
        //Winning Condition For First Column
        else if (arr[1] == arr[4] && arr[4] == arr[7])
        {
            return 1;
        }
        //Winning Condition For Second Column
        else if (arr[2] == arr[5] && arr[5] == arr[8])
        {
            return 1;
        }
        //Winning Condition For Third Column
        else if (arr[3] == arr[6] && arr[6] == arr[9])
        {
            return 1;
        }
        #endregion
        #region Diagonal Winning Condition
        else if (arr[1] == arr[5] && arr[5] == arr[9])
        {
            return 1;
        }
        else if (arr[3] == arr[5] && arr[5] == arr[7])
        {
            return 1;
        }
        #endregion
        #region Checking For Draw
        // If all the cells or values filled with X or O then any player has won the match
        else if (arr[1] != '1' && arr[2] != '2' && arr[3] != '3' && arr[4] != '4' && arr[5] != '5' && arr[6] != '6' && arr[7] != '7' && arr[8] != '8' && arr[9] != '9')
        {
            return -1;
        }
        #endregion
        else
        {
            return 0;
        }

    }
}
