using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterWalkingPotion : MonoBehaviour
{
    [SerializeField] private DisapearingPlatforms[] platforms;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                Instantiate(platforms[i], platforms[i].transform.position, Quaternion.identity);
            }
            
            Destroy(gameObject);
        }
    }
}
