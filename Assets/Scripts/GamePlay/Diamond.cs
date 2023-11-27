using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private ParticleSystem one, two, three;
    [SerializeField] private Animation animation;

    // Start is called before the first frame update
    void Start()
    {
        animation.Play();
        one.Play();
        two.Play();
        three.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
