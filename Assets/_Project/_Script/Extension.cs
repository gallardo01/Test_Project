using UnityEngine;

public static class Extension
{
    public static bool ContainLayer(this LayerMask mask, int layer)
    {
        return mask == (mask | (1 << layer));
    }
}