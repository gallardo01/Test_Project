using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] private Button PlayButton;
    [SerializeField] private GameObject loadingImage;

    private AsyncOperation loadingOperation = null;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.enabled = true;
        loadingImage.SetActive(false);
        PlayButton.onClick.AddListener(() => {
            PlayButton.enabled = false;
            int stage = PlayerPrefs.HasKey("stage") ? PlayerPrefs.GetInt("stage") : 1;
            AsyncOperation loadingOperation = SceneManager.LoadSceneAsync(stage);
            loadingImage.SetActive(true);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}