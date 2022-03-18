using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    //movement
    bool moving = true;
    bool left = true;
    float count = 0f;
    float seconds = 4.5f;
    Vector2 startLocal;
    Vector2 endLocal;


    // Start is called before the first frame update
    void Start()
    {
        startLocal = transform.parent.position;
        endLocal = new Vector2(-12.7f, 15.5f);
        shoot = GetComponent<BulletPattern>();
        delay = 0.25f;
        //execute code here
    }
    private void Update()
    {
        if(moving)
            Movement(seconds);
    }
    protected override void OnEnable()
    {
        hp = 1000;
    }
    public override void Death()
    {
        this.gameObject.SetActive(false);
    }
    protected new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
    public override void Attack()
    {
        base.Attack();
    }

    private void Movement(float s) 
    {
        if(count/s <= 1) 
        {
            moving = true;
            transform.position = Vector2.Lerp(startLocal, endLocal, count / s);
            count += Time.deltaTime;
            //add attack here
            if(attacking == null) 
            {
                attacking = Shoot(s, delay);
                StartCoroutine(attacking);
            }
        }
        else 
        {
            //change direction
            left = !left;
            if (left) 
            {
                endLocal = new Vector2(-12.7f, 15.5f);
            }
            else 
            {
                endLocal = new Vector2(12.7f, 15.5f);
            }
            if (moving)
            {
                StartCoroutine(Move_To_Spawn(1f));
            }
        }
    }
    private IEnumerator Move_To_Spawn(float s) 
    {
        moving = false;
        float count = 0;
        Vector2 startPoint = transform.position;
        yield return new WaitForSeconds(0.5f);
        while(count/s <= 1) 
        {
            transform.position = Vector2.Lerp(startPoint, startLocal, count/s);
            count += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
        transform.localPosition = Vector2.zero;
        //Do Standing Still Attack moves
        if(attacking == null) 
        {
            attacking = Shoot(10f, 0.25f);
            StartCoroutine(attacking);
        }
        yield return new WaitForSeconds(10f);
        moving = true;
        this.count = 0;
    }
    public override IEnumerator Shoot(float time, float delay) 
    {
        //remember that moving attacks are odd and stationary moves are even
        float count = 0;
        bool do_once = true;
        while(count < time && hp > 0 && GameManager.instance.player.activeSelf) 
        {
            if (moving) 
            {
                MovingAttack();
            }
            else
            {
                StationaryAttack(ref do_once, count);
            }
            yield return new WaitForSeconds(delay);
            count += delay;
            
        }
        attacking = null;
    }

    private void StationaryAttack(ref bool invoked, float activationTime) 
    {
        print(invoked);
        shoot.BasedAOE(moves[0]);
        shoot.BasedAOE(moves[2]);
        shoot.TargetPlayer(moves[4]);
        if (invoked && activationTime > 4f)
        {
            StartCoroutine(shoot.ReverseRotation(moves[0], 2f));
            invoked = false;
        }
    }
    private void MovingAttack() 
    {
        shoot.BasedAOE(moves[1]);
        shoot.BasedAOE(moves[3]);
        shoot.TargetPlayer(moves[5]);
    }
}
