using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEngine.EventSystems.EventTrigger;

public class EnermyController : MonoBehaviour
{
    [SerializeField] private GameObject[] VT;
    GameController gameController;
    private int i = 0;
    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
    }
    private void OnDisable()
    {
        i = 0;
        gameObject.transform.position = VT[0].transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        DiChuyenTuDongGiuaCacDiem();
    }
    private void DiChuyenTuDongGiuaCacDiem()
    {
        if (gameObject.transform.position == VT[i].transform.position)
        {
            if (i == 1)
            {
                i = Random.Range(2 , 4);
            }
            else if (i == 2 || i == 3)
            {
                i += 2;
            }
            else if (i == 4 || i == 5)
            {
                i = 6;
            }
            else i++;
        }
        if(i==3 || i==4)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        if(i==2 || i==5)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (i == VT.Length)
        {
            gameController.SetScore();
            gameObject.SetActive(false);
            
        }
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, VT[i].transform.position, 0.008f);
    }
}
