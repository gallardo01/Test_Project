using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] private Character yourCharacter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shooting(){
        // Bullet bullet = yourCharacter.Shooting();
        // bullet.ActiveCollider();
        yourCharacter.OnShoot();
    }

}
