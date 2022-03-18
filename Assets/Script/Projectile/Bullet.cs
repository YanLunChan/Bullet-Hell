using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    //physical properties
    private Rigidbody2D body;
    [SerializeField] private float maxSpeed;
    private float speed;
    public string tagBullet;
    //Action to reline bullet
    private UnityEvent Shoot;

    //for the wave
    private Vector3 startPosition;
    private float time;
    //debugging stuff change later
    //characteristic
    public List<Bullet_Characteristic> functions;
    private readonly Dictionary<string, UnityEvent> character = new Dictionary<string, UnityEvent>();
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        //Shoot += ShootStraight;
        foreach (Bullet_Characteristic bc in functions)
            character.Add(bc.key, bc.characteristic);
    }

    //debugging
    private void OnEnable()
    { 
        //initialize things to create wave bullets
        startPosition = transform.position;
        time = 0.0f;
        speed = maxSpeed;
    }
    public void TurnOff() 
    {
        if (gameObject.activeSelf)
        {
            BulletManager.instance.QueueBullet(tagBullet, this.gameObject);
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        Shoot?.Invoke();
    }
    //create rotation here using xcos(T) - ysin(T) = x and xsin(T) + ycos(T) = y:

    //use local y and sin(time.time) to create wave
    public void ShootStraight() => body.velocity = transform.right * speed;

    //can't find another way to do this...
    public void Wave() 
    {
        body.velocity = Vector2.zero;
        transform.position = startPosition + (speed * time * transform.right) + ( Mathf.Sin(time) * transform.up);
        time += Time.deltaTime;
    }
    public void AddExSpeed(float speed) 
    {
        this.speed += speed;
    }
    //add characteristic into shoot delegate
    public void AddChar(string key) => Shoot = character[key];
}
