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

    private void LateUpdate()
    {
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
}
