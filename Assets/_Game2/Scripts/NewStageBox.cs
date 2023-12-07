using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStageBox : MonoBehaviour
{
    public Stage stage;
    private List<ColorType> colorTypes = new List<ColorType>();

    private void OnTriggerEnter(Collider other)
    {
        if (!colorTypes.Contains(other.gameObject.GetComponent<ColorObject>().colorType))
        {
            other.gameObject.GetComponent<Player>().stage = stage;
            colorTypes.Add(other.gameObject.GetComponent<ColorObject>().colorType);
            stage.OnInit(other.gameObject.GetComponent<ColorObject>().colorType);
        }
    }
}
