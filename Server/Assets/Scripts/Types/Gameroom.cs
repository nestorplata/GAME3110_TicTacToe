using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameroom
{
    public List<Player> PlayersList = new List<Player>();
    public string GameroomID = "none";

    public bool isOnRoom(Player player)
    {
        foreach (Player n_player in PlayersList)
        {
            if(n_player.ConnectionID==player.ConnectionID &&
                n_player.Username==player.Username)
            {
                return true;
            }
        }
        return false;

    }
}
