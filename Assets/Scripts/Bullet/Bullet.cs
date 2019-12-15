using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    protected BulletParticleManager particles;
    [SerializeField]
    protected float moveSpeed = 5;
    [SerializeField]
    protected Vector2 moveDir = Vector2.one / 2;
    [SerializeField]
    protected Rigidbody2D rb;
    [SerializeField]
    protected SpriteRenderer sr;

    protected int health;
    protected float rbMagnitude;

    //bullet spawner reference
    protected BulletSpawner bSpawner;

    public int Health
    {
        get { return health; }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        health = Random.Range(1, 4); // random health points between 1 and 3
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        bSpawner = FindObjectOfType<BulletSpawner>();
        rb.AddForce(moveDir * moveSpeed);
        rbMagnitude = rb.velocity.magnitude;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // get magnitude before collision
        rbMagnitude = rb.velocity.magnitude;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // check collision with player
        if (collision.gameObject.layer == 9)
        {
            IncreaseScore.Raise();
            // decrement health and destroy bullet if health is 0
            if (--health == 0)
            {             
                particles.EnableDeath();

                //Add bullet to spawner list of dead bullets -- for bullet spawner
                bSpawner.AddBulletToDeadList(gameObject);               
            }
        }

        // maintain current magnitude after collision
        rb.velocity = rb.velocity.normalized * rbMagnitude;

        // --SUBSYSTEM --
        particles.EnableBurst();
    }

    //Enables particles for bullet -- for bullet spawner
    public void ReviveBullet()
    {
        particles.EnableLife();
    }
}
