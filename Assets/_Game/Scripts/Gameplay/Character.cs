using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Player
{

    [SerializeField] private float speed;
    [SerializeField] private GameObject targetDecoration;
    [SerializeField] private GameObject attackCircle;

    private Vector3 direction;
    private GameObject decoration;

    private void OnDisable() {
        attackCircle.SetActive(false);
        canAttack = false;

        // When end playing, decoration might be disabled already due to no enemy in range
        if (decoration) decoration.SetActive(false);
    }

    public override void OnInit()
    {
        base.OnInit();
        if (!decoration) decoration = Instantiate(targetDecoration, Vector3.zero + Vector3.up * 0.3f, Quaternion.Euler(new Vector3(90, 0, 0)));
        decoration.SetActive(true);
        attackCircle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        ToCallInUpdate();

        if (targetCount > 1)
        {
            decoration.SetActive(true);
            decoration.transform.localScale = currentTarget.localScale;
            decoration.transform.position = new Vector3(currentTarget.position.x, decoration.transform.position.y, currentTarget.position.z);
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
    }

    public override void OnDespawn()
    {
        base.OnDespawn();
        UIManager.Instance.ShowEndGameUI();
        gameObject.SetActive(false);
    }

    public override void OnKill() {
        base.OnKill();
        cameraFollow.UpSize();
    }
}
