using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform startPoint;
    [SerializeField] private ParticleSystem[] _win;
    [SerializeField] private GameObject box_close;
    [SerializeField] private GameObject box_open;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayParticleSystem()
    {
        
        for (int i = 0; i < _win.Length; i++)
        {
            if (_win[i] != null)
            {
                //Debug.Log("play");
                _win[i].Play();
            }
        }
    }
    public void setWin()
    {
        //Debug.Log("setwin");
        box_close.SetActive(false);
        box_open.SetActive(true);
    }
}
