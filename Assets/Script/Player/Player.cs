using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : Living_Entity
{
    //UI
    [SerializeField] private Player_UI UI;
    //stats
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float slowSpeed;
    [SerializeField] private GameObject[] default_shot;
    [SerializeField] private GameObject[] slow_shot;
    private GameObject[] shot;
    //Movement
    [SerializeField] private Vector2 dir;
    //Attack
    private bool attacking = false;
    private const float attack_timer = 0.2f;
    private IEnumerator att_cooldown;
    //hurt
    private bool invincible;
    //Input
    public void OnMove(InputAction.CallbackContext context) => dir = context.ReadValue<Vector2>().normalized;
    public void OnFire(InputAction.CallbackContext context) => attacking = context.performed;
    public void OnSlow(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            speed = slowSpeed;
            shot = slow_shot;

        }
        else
        {
            speed = defaultSpeed;
            shot = default_shot;
        }

    }

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        speed = defaultSpeed;
        hp = 3;
        shot = default_shot;
        UI.SetHealth(hp);
        invincible = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        if (attacking && att_cooldown == null)
            Attack();
        print(invincible);
    }
    //Corroutine
    private IEnumerator Attack_Cooldown()
    {
        yield return new WaitForSeconds(attack_timer);
        att_cooldown = null;
    }
    private void Move() 
    {
        //moving
        body.velocity = dir * speed;

    }
    //Abstract
    public override void Attack()
    {
        foreach (GameObject t in shot)
        {
            GameObject bullet = BulletManager.instance.GetBullet("Player_Standard");
            bullet.SetActive(true);
            bullet.transform.SetPositionAndRotation(t.transform.position, Quaternion.Euler(0f, 0f, 90f));
            bullet.GetComponent<Bullet>().ShootStraight();
        }
        //add delay here
        if (att_cooldown == null)
        {
            att_cooldown = Attack_Cooldown();
            StartCoroutine(att_cooldown);
        }
    }

    public override void Death()
    {
        gameObject.SetActive(false);

    }

    public override void Hurt()
    {
        if (hp > 0 && !invincible)
        {
            hp--;
            transform.position = new Vector2(0, -15);
            invincible = true;
            StartCoroutine(Blinking());
        }
        else if(hp <= 0 && !invincible)
        {
            Death();
        }
        //Update board here
        UI.SetHealth(hp);
    }
    private IEnumerator Blinking() 
    {
        int iteration = 0;
        while (iteration < 10) 
        {
            GetComponent<SpriteRenderer>().enabled = false;
            yield return new WaitForSeconds(0.2f);
            GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(0.2f);
            iteration++;
        }
        invincible = false;
        yield return null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<Bullet>())
        {
            collision.collider.GetComponent<Bullet>().TurnOff();
            Hurt();
        }
        else if (collision.collider.GetComponent<Enemy>()) 
        {
            Hurt();
        }
    }


}
