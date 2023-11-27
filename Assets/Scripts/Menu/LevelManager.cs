using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private List<Button> buttons;

    private int currentStage;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("stage")) PlayerPrefs.SetInt("stage", 0);
        currentStage = PlayerPrefs.GetInt("stage");
        for (int i = 0; i < buttons.Count; i++) {
            addOnClick(buttons[i], i);
        }
    }

    private void addOnClick (Button b, int index) {
        b.onClick.AddListener(() => {
            if (index < currentStage) SceneManager.LoadSceneAsync(index + 1);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
