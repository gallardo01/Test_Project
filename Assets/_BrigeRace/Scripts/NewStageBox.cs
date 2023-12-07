using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStageBox : MonoBehaviour
{
    public Stage stage;

    private List<ColorType> colorTypes = new List<ColorType>();
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        if (!colorTypes.Contains(other.gameObject.GetComponent<ColorObject>().colorType))
        {
            other.gameObject.GetComponent<PlayerController>().stage = stage;
            colorTypes.Add(other.gameObject.GetComponent<ColorObject>().colorType);
            stage.OnInit(other.gameObject.GetComponent<ColorObject>().colorType);
        }
    }
}
