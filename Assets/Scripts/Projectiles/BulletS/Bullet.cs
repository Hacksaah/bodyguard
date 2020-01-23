using System.Collections;
using UnityEngine;

public class Bullet : Projectile
{
    public BulletAudio audio;

    [SerializeField]
    protected BulletParticleManager particles;
    [SerializeField]
    protected float moveSpeed = 5;
    protected float rbMagnitude;


    // Start is called before the first frame update
    override protected void Awake()
    {
        base.Awake();
        audio.audioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        // get magnitude before collision
        rbMagnitude = rb.velocity.magnitude;
    }

    override public void PlayerHit(Vector2 hitDir)
    {
        //if the player hits the bullet from behind...
        if (Vector2.Angle(hitDir, rb.velocity) < 66)
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
            rb.velocity = -rb.velocity * 1.35f;
            IncreaseScore.Raise();
            particles.ChangeParticleColor(health - 1);
            audio.PlayClinkSound();
        }
    }

    //Enables particles for bullet -- for bullet spawner
    override public void ReviveProjectile(Vector2 direction, int HP)
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

    void OnCollisionEnter2D(Collision2D collision)
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
        pSpawner.AddBulletToDeadList(gameObject);
        StartCoroutine(DeactivateCollider());
    }

    IEnumerator DeactivateCollider()
    {
        coll.enabled = false;
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector2.up * 60;
    }

    
}
