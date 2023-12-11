using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{

    [SerializeField] private Brick brick;

    private bool active;

    public ColorType color { get => brick.ColorType; }

    // Start is called before the first frame update
    void Start()
    {
        active = false;
        brick.gameObject.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fill(ColorType colorType) {
        active = true;
        brick.gameObject.SetActive(active);
        brick.ChangeColor(colorType);
    }

    public bool Filled() {
        return active;
    }
}
