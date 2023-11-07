using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(DestroySelf), 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") other.GetComponent<Player>().Heal(30);
    }
}
