using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Swipe : MonoBehaviour
{

    [SerializeField] private Player player;

    private Vector2 startTouchPosition, endTouchPosition;
    private float verticalChange, horizontalChange;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (player.Direction != Vector3.zero) return;
        if (Input.GetMouseButtonDown(0)) {
            startTouchPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;

            verticalChange = Mathf.Abs(endTouchPosition.y - startTouchPosition.y);
            horizontalChange = Mathf.Abs(endTouchPosition.x - startTouchPosition.x);

            if (verticalChange > horizontalChange) {
                if (endTouchPosition.y > startTouchPosition.y) player.MoveUp();
                else if (endTouchPosition.y < startTouchPosition.y) player.MoveBack();
            } else {
                if (endTouchPosition.x < startTouchPosition.x) player.MoveLeft();
                else if (endTouchPosition.x > startTouchPosition.x) player.MoveRight();
            }
        }
    }
}
