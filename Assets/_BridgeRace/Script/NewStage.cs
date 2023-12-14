using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStage : MonoBehaviour
{
    // Start is called before the first frame update
    public Stage stage;
    private List<ColorType> colorTypes = new List<ColorType>();
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Player" && !colorTypes.Contains(other.gameObject.GetComponent<ColorObject>().colorType)) {
            other.gameObject.GetComponent<Character>().stage = stage;
            colorTypes.Add(other.gameObject.GetComponent<ColorObject>().colorType);
            stage.OnInit(other.gameObject.GetComponent<ColorObject>().colorType);
        }
    }
}
