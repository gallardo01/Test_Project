using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRange : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private float y;
    [SerializeField] private Transform character;
    [SerializeField] private float ThetaScale = 0.01f;
    [SerializeField] private float radius = 3f;
    [SerializeField] private float width;
    [SerializeField] private Player player;
    
    private int Size;
    private float Theta = 0f;

    void Update()
    {
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        lineRenderer.positionCount = Size;
        for (int i = 0; i < Size; i++)
        {
            Theta += 2.0f * Mathf.PI * ThetaScale;
            float x = radius * Mathf.Cos(Theta);
            float z = radius * Mathf.Sin(Theta);
            lineRenderer.SetPosition(i, new Vector3(x, y, z) + character.transform.position);
        }

        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Constants.PLAYER_TAG) player.AddTarget(other);
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == Constants.PLAYER_TAG) player.RemoveTarget(other);
    }
}
