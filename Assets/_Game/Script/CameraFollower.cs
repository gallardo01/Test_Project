using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform TF;
    public Player player;
    public Camera camera;
    [SerializeField] Vector3 offset;
    [SerializeField] Vector3 offsetInShop;

    [SerializeField] Material BlurMaterial;
    private Material OldMaterial;
    [SerializeField] LayerMask ObstacleLayer;
    Transform currentObstacle;

    bool IsObstacle;

    private void LateUpdate()
    {
        CheckWall();
        if (GameManager.IsState(GameState.ShopSkin))
        {
            camera.orthographicSize = 8f;
            TF.position = Vector3.Lerp(TF.position, player.transform.position + offsetInShop, Time.deltaTime * 5f);
            TF.rotation = Quaternion.Euler(15f, 0, 0);
            return;

        }
        TF.position = Vector3.Lerp(TF.position, player.transform.position + offset + new Vector3(0, player.attackRange*1.5f, - player.attackRange*1.5f), Time.deltaTime * 5f);
        TF.rotation = Quaternion.Euler(45f, 0, 0);

    }

    public void CheckWall()
    {
        Vector3 PlayerPos = Camera.main.WorldToScreenPoint(player.transform.position);

        Ray ray = Camera.main.ScreenPointToRay(PlayerPos);
        if(Physics.Raycast(ray,out RaycastHit hit, Mathf.Infinity, ObstacleLayer))
        {
            if (!IsObstacle || currentObstacle != hit.transform)
            {
                if(OldMaterial != null)
                {
                    currentObstacle.GetComponent<MeshRenderer>().material = OldMaterial;
                }
                IsObstacle = true;
                currentObstacle = hit.transform;
                OldMaterial = currentObstacle.GetComponent<MeshRenderer>().material;
                currentObstacle.GetComponent<MeshRenderer>().material = BlurMaterial;
            }
        }
        else
        {
            if(OldMaterial != null)
            {
                currentObstacle.GetComponent<MeshRenderer>().material = OldMaterial;
                currentObstacle = null;
                OldMaterial = null;
            }
            IsObstacle = false;
        }
    }
}
