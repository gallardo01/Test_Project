using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class WinPillar : MonoBehaviour, IMovable
{
    [SerializeField] private Transform winPos;
    [SerializeField] private GameObject particle;
    public void OnHit(Player player)
    {
        player.IncrementHeight(-1000);
        player.OnWin();
        player.transform.position = winPos.position;

        Vector3 newRotation = player.transform.eulerAngles;
        newRotation.y += 215;
        player.transform.eulerAngles = newRotation;

        particle.SetActive(true);
    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
