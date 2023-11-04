using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform Base;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    
    private int score = 0;

    private GameController() {

    }

    private static GameController instance;

    public static GameController Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(CreateBullet), 0.00001f);
        Invoke(nameof(CreatePlayer), 1f);
        textMeshPro.text = 0.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A)) Debug.Log("A");
        if (Input.GetKey(KeyCode.B)) Debug.Log("B");
        if (Input.GetKey(KeyCode.C)) Debug.Log("C");


    }

    private void CreateBullet()
    {
        GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
        if (bullet != null)
        {
            bullet.transform.position = Base.position;
            bullet.SetActive(true);
        }

        Invoke(nameof(CreateBullet), 2f);
    }

    private void CreatePlayer()
    {
        GameObject player = ObjectPool.SharedInstance.GetPlayer();
        if (player != null)
        {
            player.transform.position = Base.position;
            player.SetActive(true);
        }

        Invoke(nameof(CreatePlayer), 3f);
    }

    public void IncreaseScore() {
        score++;
        textMeshPro.text = score.ToString();
    }
}
