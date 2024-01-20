using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : AbsCharacter
{

    public Animator animator;
    private string currentAnim;
    public Transform body;
    public LayerMask groundLayer;
    public List<Character> targets = new List<Character>();
    public Character target;
    [SerializeField] GameObject bulletPrefabs;
    [SerializeField] GameObject weapon;
    // Start is called before the first frame update
    private int score = 1;
    [SerializeField] GameObject indicatorPoint;
    public TargetIndicator targetIndicator;
    public Skin skin;

    void Start()
    {

    }

    public void ChangeWeapon(int index)
    {
        // set bullet weapon = ...
        weapon = skin.ChangeWeapon(index);
    }
    public void Throw()
    {
        //weapon.SetActive(false);
            Bullet bullet = Instantiate(bulletPrefabs, transform.position, Quaternion.identity).GetComponent<Bullet>();
            bullet.OnInit(this, target.transform);
            weapon.GetComponent<Weapon>().Throw();
    }

    public bool CanMove(Vector3 point)
    {
        bool canMove = true;
        return canMove;
    }

    public void UpdatePoints()
    {
        score++;
        targetIndicator.SetScore(score);
        transform.localScale = new Vector3(1f + (score-1)*0.2f, 1f + (score - 1) * 0.2f, 1f + (score - 1) * 0.2f);
    }

    public virtual void changeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            animator.ResetTrigger(currentAnim);
            currentAnim = animName;
            animator.SetTrigger(currentAnim);
        }
    }

    public virtual void AddTarget(Character target)
    {
        targets.Add(target);
    }

    public virtual void RemoveTarget(Character target)
    {
        targets.Remove(target);
        //this.target = null;
    }

    public Character GetTargetInRange()
    {
        if (targets.Count > 0)
        {
            target = targets[Random.Range(0, targets.Count)];
            return target;
        }
        return null;
    }

    public override void OnInit()
    {
        targetIndicator = LevelManager.Ins.CreateIndicatorPanel(indicatorPoint.transform);
    }
    public override void OnDespawn()
    {
    }
    public override void OnAttack()
    {
    }
    public override void OnDeath()
    {
        targetIndicator.gameObject.SetActive(false);
        LevelManager.Ins.InitCharacterAlive();
        changeAnim("dead");
    }
}
