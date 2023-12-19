using System.Collections.Generic;
using UnityEngine;

public static class Cache
{

private static Dictionary<Collider, Player> players = new Dictionary<Collider, Player>();

    public static Player GetPlayer(Collider collider)
    {
        if (!players.ContainsKey(collider))
        {
            players.Add(collider, collider.GetComponent<Player>());
        }

        return players[collider];
    }
}