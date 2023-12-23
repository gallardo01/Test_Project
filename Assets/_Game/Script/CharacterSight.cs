using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterSight : MonoBehaviour
{

    [SerializeField] Character character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.AddTarget(other.gameObject.GetComponent<Character>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            character.RemoveTarget(other.gameObject.GetComponent<Character>());
        }
    }
}
    