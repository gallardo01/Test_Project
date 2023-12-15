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
        // if(stage.gameObject.name != "StageWin"){
            if (!colorTypes.Contains(other.gameObject.GetComponent<ColorObject>().colorType))
            {
                // if(other.gameObject.GetComponent<Character>().stage != null){
                //     other.gameObject.GetComponent<Character>().stage.StopSpawnBrick(other.gameObject.GetComponent<ColorObject>().colorType);
                // }
                other.gameObject.GetComponent<Character>().planeStage = stage.planeStage;
                other.gameObject.GetComponent<Character>().stage = stage;
                colorTypes.Add(other.gameObject.GetComponent<ColorObject>().colorType);
                stage.OnInit(other.gameObject.GetComponent<ColorObject>().colorType);
            }
        // }
        // if(stage.gameObject.name == "StageWin"){
        //     Debug.Log("Win");
        //     LevelManager.Ins.WinGame();
        // }
    }
}
