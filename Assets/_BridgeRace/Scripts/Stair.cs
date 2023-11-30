using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{

    [SerializeField] GameObject brick;

    private bool active;

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        brick.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill(Color color) {
        active = true;
        brick.SetActive(active);
        brick.GetComponent<MeshRenderer>().material.color = color;
    }

    public bool Filled() {
        return active;
    }
}
