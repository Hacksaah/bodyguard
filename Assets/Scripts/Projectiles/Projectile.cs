using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameEvent IncreaseScore;
    public GameEvent IncreaseCritScore;

    [SerializeField]
    protected Rigidbody2D rb;
    protected CircleCollider2D coll;
    protected int health;

    public int Health
    {
        get { return health; }
    }

    //bullet spawner reference
    protected ProjectileSpawner bSpawner;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bSpawner = FindObjectOfType<ProjectileSpawner>();
        coll = GetComponent<CircleCollider2D>();
    }

    public virtual void PlayerHit(Vector2 hitDir)
    {

    }

    public virtual void ReviveProjectile(Vector2 direction, int HP)
    {

    }
}
