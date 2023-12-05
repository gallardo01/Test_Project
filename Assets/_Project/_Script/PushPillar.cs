using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPillar : Pillar
{
    [SerializeField] private Transform pusherRoot;
    [SerializeField] private PushDirection pushDir = PushDirection.DownLeft;
    [SerializeField] private Animator animator;

    public override void OnHit(Player player)
    {
        base.OnHit(player);
        
        Direction nextDirection = ConvertNextDirection(player.currentDir);
        player.StashDirection(nextDirection);
        
        animator.SetTrigger("Push");
    }

    private Direction ConvertNextDirection(Direction direction)
    {
        switch (pushDir)
        {
            case PushDirection.DownLeft:
                if (direction == Direction.Up) return Direction.Left;
                if (direction == Direction.Right) return Direction.Down;
                break;
            case PushDirection.DownRight:
                if (direction == Direction.Up) return Direction.Right;
                if (direction == Direction.Left) return Direction.Down;
                break;
            case PushDirection.UpLeft:
                if (direction == Direction.Down) return Direction.Left;
                if (direction == Direction.Right) return Direction.Up;
                break;
            case PushDirection.UpRight:
                if (direction == Direction.Down) return Direction.Right;
                if (direction == Direction.Left) return Direction.Up;
                break;
        }

        return Direction.None;
    }

    enum PushDirection
    {
        DownLeft,
        DownRight,
        UpLeft,
        UpRight,
    }
        
    private void OnValidate()
    {
        if (!pusherRoot) return;
        Vector3 rotation = pusherRoot.eulerAngles;
            
        switch (pushDir)
        {
            case PushDirection.DownLeft:
                rotation.y = 0;
                break;
            case PushDirection.DownRight:
                rotation.y = 270;
                break;
            case PushDirection.UpLeft:
                rotation.y = 90;
                break;
            case PushDirection.UpRight:
                rotation.y = 180;
                break;
        }

        pusherRoot.eulerAngles = rotation;
    }
}
