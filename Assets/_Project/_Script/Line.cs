using System;
using UnityEngine;

public class Line : MonoBehaviour, IMovable
{
    [SerializeField] private Renderer brickRenderer;
    [SerializeField] private bool hasBrick;

    private void Awake()
    {
        brickRenderer.enabled = false;
    }

    public void OnHit(Player player)
    {
        if (hasBrick) return;
        
        player.IncrementHeight(-1);
        
        brickRenderer.enabled = true;
        hasBrick = true;
    }
}