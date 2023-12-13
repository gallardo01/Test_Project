using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBox : MonoBehaviour
{

    public Stage stage;


    private void OnTriggerEnter(Collider other)
    {
        Character character = other.GetComponent<Character>();
        character.stage = stage;
        if (!stage.colorList.Contains(character.colorType))
        {
            stage.colorList.Add(character.colorType);
            stage.OnInitBrick(character.colorType);

        }
    }
}
