using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] Character character;
    // [SerializeField] protected Range range;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        Vector3 point = new Vector3();
        character.Attack(point);
    }

    public void EndShoot(){
        Debug.Log("EndShoot");
        character.isAttack = false;
    }

    public void Disable(){
        if (character.GetComponent<Bot>() != null)
        {
            character.GetComponent<Bot>().ReturnPool();
        }
    }

}
