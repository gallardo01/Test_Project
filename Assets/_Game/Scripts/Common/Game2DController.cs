using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

// Game2DController : Singleton<Game2DController>
// Game2DController.Instance

public class Game2DController : MonoBehaviour
{
    
    [SerializeField] private TilemapCollider2D waterCollider;
    [SerializeField] private TextMeshProUGUI timeRemaining;
    [SerializeField] private Collider2D playerCollider;

    public TilemapCollider2D WaterCollider { get => waterCollider; }
    public TextMeshProUGUI TimeRemaining { get => timeRemaining; }
    public Collider2D PlayerCollider { get => playerCollider; }

    private static Game2DController _instance;

    public static Game2DController Instance { get { return _instance; } }
    public int stage;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("stage")) PlayerPrefs.SetInt("stage", 1);

        stage = PlayerPrefs.GetInt("stage");

        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        } else {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        waterCollider.enabled = false;
        timeRemaining.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
