using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    [SerializeField] private Character yourCharacter;
    // // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Character"){
            yourCharacter.OnAttack(other.gameObject.transform.position);
        }
    }

}
