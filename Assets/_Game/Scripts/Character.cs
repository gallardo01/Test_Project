using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Player
{

    [SerializeField] private GameObject targetDecoration;

    private Vector3 direction;
    private GameObject decoration;

    private void Start()
    {
        decoration = Instantiate(targetDecoration, Vector3.zero + Vector3.up * 0.3f, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (targets.Count > 0)
        {
            decoration.SetActive(true);
            decoration.transform.position = new Vector3(targets[0].position.x, decoration.transform.position.y, targets[0].transform.position.z);
        } else {
            decoration.SetActive(false);
        }

        direction = JoystickControl.direct;

        if (direction != Vector3.zero)
        {
            ChangeAnim(Constants.RUN_ANIM);
            player.LookAt(direction + player.position);
        }
        else if (currentAnim != Constants.ATTACK_ANIM) ChangeAnim(Constants.IDLE_ANIM);

        player.Translate(direction * speed * Time.deltaTime, Space.World);

        ToCallInUpdate();

    }

    public override void OnDespawn()
    {
        Application.Quit();
    }
}
