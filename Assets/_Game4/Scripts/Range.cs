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
        if(other.tag == "Bot" && yourCharacter.GetComponent<Player>() != null && yourCharacter.canMove){
            yourCharacter.OnAttack(other.gameObject.transform.position);
        }
        if(other.tag == "Player" && yourCharacter.GetComponent<Bot>() != null && yourCharacter.canMove){
            yourCharacter.OnAttack(other.gameObject.transform.position);
            
            yourCharacter.GetComponent<Bot>().ChangeState(new AttackState());
        }
    }

}
