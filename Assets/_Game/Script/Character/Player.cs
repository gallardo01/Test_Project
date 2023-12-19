using UnityEngine;


public class Player : Character
{

    [SerializeField] FloatingJoystick joystick;
    public RangeAttack rangeAttack;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
        ChangAnim(Constants.ANIM_IDLE);
        isAttack = false;
        isRun = false;
    }

    // Update is called once per frame
    void Update()
    {

        direct = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        if (!isRun)
        {
            if (CheckEnemy())
            {

                Attack();
            }
            else
            {

                ChangAnim(Constants.ANIM_IDLE);
            }
            if (Vector3.Distance(direct, Vector3.zero) >= 0.00001f)
            {
                isRun = true;

            }
        }
        else
        {
            ChangAnim(Constants.ANIM_RUN);
            transform.position += direct * speed * Time.deltaTime;
            playerSkin.transform.forward = direct;
            if (Vector3.Distance(direct, Vector3.zero) <= 0.00001f) isRun = false;

        }





    }

}
