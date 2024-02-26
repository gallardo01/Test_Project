using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirDrop : MonoBehaviour
{
    [SerializeField] LayerMask GroundLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 0 && !GameManager.IsState(GameState.PauseGame))
        {
            transform.position += Vector3.down * 2 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.TAG_BOT) || other.CompareTag(Constants.TAG_PLAYER))
        {

            this.gameObject.SetActive(false);
            Character character = Cache.GetScript(other);
            character.isUlti = true;
            character.BuffUlti();
            LevelManager.Instance.timAirDrop.Start(LevelManager.Instance.DropGift, 5f);
        }
    }
}
