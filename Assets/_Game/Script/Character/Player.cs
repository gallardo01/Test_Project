using UnityEngine;

public class Player : Character
{

    [SerializeField] FloatingJoystick joystick;
    private Vector3 direct;
    public bool isAttack;
    public bool isRun;

    // Start is called before the first frame update
    void Start()
    {
        ChangAnim(Constants.ANIM_IDLE);
        isAttack = false;
        isRun = false;
    }

    // Update is called once per frame
    void Update()
    {

        direct = new Vector3(joystick.Horizontal, 0, joystick.Vertical);
        //Debug.Log(Vector3.Distance(direct, Vector3.zero));
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
    public void Attack()
    {
        if (Vector3.Distance(direct, Vector3.zero) >= 0.00001f)
        {
            isRun = true;

        }
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, characterLayer);
        if (enemies.Length > 1)
        {
            ChangAnim(Constants.ANIM_ATTACK);
            target = Cache.GetScript(enemies[0]);
            positionTarget = target.transform.position;
            if (target != null)
            {

                isAttack = true;
                ThrowWeapon(positionTarget);

            }

        }

    }
    public bool CheckEnemy()
    {

        Collider[] Enemys = Physics.OverlapSphere(transform.position, attackRange, characterLayer);
        return Enemys.Length > 1;
    }
}
