using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Living_Entity
{
    protected Pattern_Variables[] moves;
    protected BulletPattern shoot;
    public Transform parent;
    [SerializeField] protected float time, delay;
    [SerializeField] private bool target;
    protected IEnumerator attacking;
    // Start is called before the first frame update
    void Awake()
    {
        shoot = GetComponent<BulletPattern>();
        anime = GetComponent<Animator>();
    }
    //Coroutine
    public virtual IEnumerator Shoot(float time, float delay) 
    {
        float count = 0;
        while(count < time && gameObject.activeInHierarchy) 
        {
            if (!target)
                shoot.BasedAOE(moves[0]);
            else
                shoot.TargetPlayer(moves[0]);
            yield return new WaitForSeconds(delay);
            count += delay;
            //rotation
            //if (time / 2 == count)
            //    StartCoroutine(shoot.ReverseRotation(moves[0],2f));
        }
        attacking = null;
    }
    public void Despawn() 
    {
        this.gameObject.SetActive(false);
        returnParent();
    }
    public void setMoves(Pattern_Variables[] p)
    {
        moves = new Pattern_Variables[p.Length];
        for (int i = 0; i < p.Length; i++) 
        {
            moves[i] = new Pattern_Variables(p[i]);
        }
    }
    public void setPathing(int num) => anime.SetInteger("Pathing", num);
    public void setTarget(bool shoot) => target = shoot;

    //attack trigger through animation
    public override void Attack()
    {
        if (attacking == null) 
        {
            attacking = Shoot(this.time, this.delay);
            StartCoroutine(attacking);
        }
        
    }

    public override void Death()
    {
        anime.SetBool("Alive", false);
        returnParent();
        //if they're in the middle of attacking
        attacking = null;
    }

    public override void Hurt()
    {
        if (--hp < 0) 
        {
            Death();
        }
    }

    protected virtual void OnEnable()
    {
        hp = 2;
        anime.SetBool("Alive", true);
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Bullet>())
        {
            Hurt();
            collision.collider.GetComponent<Bullet>().TurnOff();
        }
    }
    public void returnParent() => transform.parent = parent;
}
