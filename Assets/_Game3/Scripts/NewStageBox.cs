using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewStageBox : MonoBehaviour
{
    public Stage stage;
    private List<ColorType> colorTypes = new List<ColorType>();
    void OnTriggerEnter(Collider other){
        // Character character 
        if(!colorTypes.Contains(other.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ColorObject>().colorType)){
            // Debug.Log("Human");
            other.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().stage = stage;
            // other.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<PlayerController>().ChangeColor((ColorType)stage.intColorTypeList[0]);
            colorTypes.Add(other.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ColorObject>().colorType);
            stage.OnInit(other.gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<ColorObject>().colorType);
        }
    }
}
