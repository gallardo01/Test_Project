using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

// Game2DController : Singleton<Game2DController>
// Game2DController.Instance

public class Game2DController : MonoBehaviour
{
    
    [SerializeField] private TilemapCollider2D waterCollider;
    [SerializeField] private TextMeshProUGUI timeRemaining;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Image ProgressBar;

    public TilemapCollider2D WaterCollider { get => waterCollider; }
    public TextMeshProUGUI TimeRemaining { get => timeRemaining; }
    public Collider2D PlayerCollider { get => playerCollider; }

    private static Game2DController _instance;
    private float progress;

    public static Game2DController Instance { get { return _instance; } }

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("stage")) PlayerPrefs.SetInt("stage", 1);


        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }

        progress = 0;
    }

    public void UpdateProgress() {
        progress++;
        ProgressBar.fillAmount = progress / 100;
        if (progress == 100) {
            // StartCoroutine(ToNextScene());
        }
    }

    // IEnumerator ToNextScene() {
    //     PlayerPrefs.SetInt("stage", 2);
    //     AsyncOperation operation = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("stage"));
    //     yield return null;
    // }

    // Start is called before the first frame update
    void Start()
    {
        waterCollider.enabled = false;
        timeRemaining.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            progress = 98;
            UpdateProgress();
        }
    }
}
