using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterPotion : MonoBehaviour
{
    private TilemapCollider2D waterCollider;
    private TextMeshProUGUI timeRemaining;
    private static WaterPotion currentActivePotion;
    private float time;
    private bool activated;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        currentActivePotion = null;
        waterCollider = Game2DController.Instance.WaterCollider;
        timeRemaining = Game2DController.Instance.TimeRemaining;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            time = (time -= Time.deltaTime) < 0 ? 0 : time;
            timeRemaining.SetText(String.Format("{0:N3}", time));
            if (time == 0)
            {
                waterCollider.enabled = false;
                timeRemaining.gameObject.SetActive(false);
                currentActivePotion = null;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            activated = true;

            // Reset effect
            if (!currentActivePotion)
            {
                currentActivePotion = this;
            }
            else
            {
                Destroy(currentActivePotion);
            }

            // Moving on water
            waterCollider.enabled = true;

            // Destroy after consume
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            // Timer
            timeRemaining.gameObject.SetActive(true);
            time = 10;
            timeRemaining.SetText(String.Format("{0:N3}", time));
        }
    }

}
