using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletParticleManager particles;
    public float moveSpeed = 5;
    public Vector2 moveDir = Vector2.one / 2;

    protected int health;
    protected float rbMagnitude;
    protected Rigidbody2D rb;

    private SpriteRenderer sr;

    public int Health
    {
        get { return health; }
    }

    // Start is called before the first frame update
    protected void Start()
    {
        health = Random.Range(1, 4); // random health points between 1 and 3
        Debug.Log("Health: " + health);
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(moveDir * moveSpeed);
        rbMagnitude = rb.velocity.magnitude;
    }

    // Update is called once per frame
    protected void Update()
    {
        // get magnitude after collision
        rbMagnitude = rb.velocity.magnitude;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        // check collision with player
        if (collision.gameObject.layer == 9)
        {
            // decrement health and destroy bullet if health is 0
            if (--health == 0)
            {
                particles.EnableDeath();
            }
        }

        // maintain current magnitude after collision
        rb.velocity = rb.velocity.normalized * rbMagnitude;

        // --SUBSYSTEM --
        particles.EnableBurst();
    }
}
