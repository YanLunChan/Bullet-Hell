using UnityEngine;

public abstract class Living_Entity : MonoBehaviour
{
    [SerializeField] protected int hp;
    [SerializeField] protected float speed;
    protected Rigidbody2D body;
    protected Animator anime;
    public abstract void Attack();
    public abstract void Hurt();
    public abstract void Death();
}
