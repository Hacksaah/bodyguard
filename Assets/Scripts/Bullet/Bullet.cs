using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletAudio audio;
    public GameEvent IncreaseScore;
    public GameEvent IncreaseCritScore;

    [SerializeField]
    protected BulletParticleManager particles;
    [SerializeField]
    protected float moveSpeed = 5;
    [SerializeField]
    protected Rigidbody2D rb;
    protected CircleCollider2D coll;

    protected int health;
    protected float rbMagnitude;

    //bullet spawner reference
    protected BulletSpawner bSpawner;

    public int Health
    {
        get { return health; }
    }

    // Start is called before the first frame update
    protected virtual void Awake()
    {       
        rb = GetComponent<Rigidbody2D>();
        bSpawner = FindObjectOfType<BulletSpawner>();        
        audio.audioSource = GetComponent<AudioSource>();
        coll = GetComponent<CircleCollider2D>();        
    }

    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // get magnitude before collision
        rbMagnitude = rb.velocity.magnitude;
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        // if bullet collides with environment
        if(collision.gameObject.layer == 8)
        {
            audio.PlayBounceSound();
        }

        // maintain current magnitude after collision
        rb.velocity = rb.velocity.normalized * rbMagnitude;

        // --SUBSYSTEM --
        particles.EnableBurst();
    }

    public void PlayDeathNoSound()
    {
        particles.EnableDeath();
        rb.simulated = false;
        //Add bullet to spawner list of dead bullets -- for bullet spawner
        bSpawner.AddBulletToDeadList(gameObject);
        StartCoroutine(DeactivateCollider());
    }

    //Enables particles for bullet -- for bullet spawner
    public virtual void ReviveBullet(Vector2 direction, int HP)
    {        
        StopAllCoroutines();
        rb.simulated = true;
        rb.bodyType = RigidbodyType2D.Static;
        rb.bodyType = RigidbodyType2D.Dynamic;
        audio.PlaySpawnSound();
        particles.EnableLife();
        coll.enabled = true;
        health = HP;
        particles.ChangeParticleColor(health - 1);
        rb.AddForce(direction * moveSpeed);
        rbMagnitude = rb.velocity.magnitude;
    }

    IEnumerator DeactivateCollider()
    {
        coll.enabled = false;
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector2.up * 60;
    }

    public virtual void PlayerHit(Vector2 hitDir)
    {
        //if the player hits the bullet from behind...
        if(Vector2.Angle(hitDir, rb.velocity) > 115)
        {
            health = 0;
            IncreaseCritScore.Raise();
            PlayDeathNoSound();
            audio.PlayClinkSound();
        }
        // decrement health and destroy bullet if health is 0
        else if (--health == 0)
        {
            IncreaseScore.Raise();
            PlayDeathNoSound();
            audio.PlayClinkSound();
        }
        else
        {
            rb.velocity = -rb.velocity*1.35f;
            IncreaseScore.Raise();
            particles.ChangeParticleColor(health - 1);
            audio.PlayClinkSound();
        }
    }
}
