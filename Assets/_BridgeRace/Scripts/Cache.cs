using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Cache
{
    private static Dictionary<Collider, Player> characters = new Dictionary<Collider, Player>();
    private static Dictionary<Collider, Brick> bricks = new Dictionary<Collider, Brick>();
    private static Dictionary<Transform, Brick> still_bricks = new Dictionary<Transform, Brick>();
    private static Dictionary<Collider, Stair> stairs = new Dictionary<Collider, Stair>();

    public static Player GetPlayer(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Player>());
        }

        return characters[collider];
    }

    public static Brick GetBrick(Collider collider) {
        if (!bricks.ContainsKey(collider)) {
            bricks.Add(collider, collider.GetComponent<Brick>());
        }
        return bricks[collider];
    }

    public static Brick GetBrick(Transform transform) {
        if (!still_bricks.ContainsKey(transform)) {
            still_bricks.Add(transform, transform.GetComponent<Brick>());
        }
        return still_bricks[transform];
    }

    public static Stair GetStair(Collider collider) {
        if (!stairs.ContainsKey(collider)) {
            stairs.Add(collider, collider.GetComponent<Stair>());
        }
        return stairs[collider];
    }
}