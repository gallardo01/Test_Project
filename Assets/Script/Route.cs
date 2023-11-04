using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Route
{
    public Vector3 startPos;
    public Queue<Vector3> positions;

    private const int MAX_ITERATION = 20;

    private MoveNode _currentNode;
    
    public Route(MoveNode startNode, MoveNode endNode = null)
    {
        positions = new Queue<Vector3>();
        startPos = startNode.transform.position;
        _currentNode = startNode;

        for (int i = 0; i < MAX_ITERATION; i++)
        {
            var node = _currentNode.GetNextNode();
            positions.Enqueue(node.transform.position);
            _currentNode = node;

            if (node == endNode)
                break;
        }
    }
}