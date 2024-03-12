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

    private static Dictionary<GameObject, Renderer> rendererSkin = new Dictionary<GameObject, Renderer>();

    public static Renderer GetRenderer(GameObject go) {
        if (!rendererSkin.ContainsKey(go)) {
            rendererSkin.Add(go, go.GetComponent<Renderer>());
        }

        return rendererSkin[go];
    }
}