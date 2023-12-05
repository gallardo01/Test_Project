using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour, IMovable
{
    [SerializeField] private Renderer brickRenderer;
    [SerializeField] private bool hasBrick = true;
    public virtual void OnHit(Player player)
    {
        if (!hasBrick) return;
        
        player.IncrementHeight(1);
        
        brickRenderer.enabled = false;
        hasBrick = false;
    }
}
