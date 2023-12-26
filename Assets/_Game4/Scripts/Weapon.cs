using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnabled(){
        bullet.SetActive(true);
    }

    public void Throw(){
        bullet.SetActive(false);
        Invoke(nameof(OnEnabled), 0.3f);
    }

}
