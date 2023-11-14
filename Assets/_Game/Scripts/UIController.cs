using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Image ProgressBar;

    // Start is called before the first frame update
    void Start()
    {
        PlayButton.enabled = true;
        ProgressBar.enabled = false;
        PlayButton.onClick.AddListener(() =>
        {
            StartCoroutine(Loading());
        });
    }

    IEnumerator Loading()
    {
        int stage = PlayerPrefs.HasKey("stage") ? PlayerPrefs.GetInt("stage") : 1;
        AsyncOperation loadLevelOp = SceneManager.LoadSceneAsync(stage);
        ProgressBar.enabled = true;
        PlayButton.enabled = false;
        while (!loadLevelOp.isDone)
        {
            ProgressBar.fillAmount = Mathf.Clamp01(loadLevelOp.progress);
            yield return null;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}