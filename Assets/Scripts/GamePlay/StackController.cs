using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StackController : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform brickParent;
    [SerializeField] private GameObject brickPrefab;
    [SerializeField] private LayerMask brickLayer;
    [SerializeField] private GameObject model;
    [SerializeField] private Player player;
    [SerializeField] private LayerMask lineLayer;

    private List<GameObject> bricks;
    public static StackController Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private float position;

    // Start is called before the first frame update
    void Start()
    {
        bricks = new List<GameObject>();
        position = -0.35f;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(start.position, Vector3.down, out hit, 1f, brickLayer)) {
            GameObject brick = Instantiate(brickPrefab, brickParent);
            bricks.Add(brick);
            brick.transform.localScale = new Vector3(brick.transform.localScale.x, brick.transform.localScale.y, 0.5f);
            brick.transform.localPosition = new Vector3(0, position, 0);
            position += 0.175f;
            hit.collider.gameObject.SetActive(false);
        } else if (Physics.Raycast(start.position, Vector3.down, out hit, 2f, lineLayer)) {
            hit.collider.gameObject.GetComponent<Line>().ShowBrick();
            position -= 0.175f;
            if (bricks.Count > 0) {
                Destroy(bricks[bricks.Count - 1]);
                bricks.RemoveAt(bricks.Count - 1);
            } else {
                Debug.Log("Lose");
                player.Stop();
                Invoke(nameof(Menu), 2f);
            }
            
        }

        model.transform.localPosition = new Vector3(0, position - 0.25f, 0);  
    }

    private void Menu() {
        SceneManager.LoadScene(0);
    }

    public void Finish() {
        brickParent.gameObject.SetActive(false);
        position = -0.1f;
    }
}
