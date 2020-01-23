using UnityEngine;
using System.Collections;

public class BoidBullet : Projectile
{    
    [SerializeField]
    private GameObject target;

    public BulletAudio audio;
    [SerializeField]
    protected BulletParticleManager particles;
    [SerializeField]
    protected float moveSpeed = 5;
    protected float rbMagnitude;

    // Start is called before the first frame update
    override protected void Awake()
    {
        // everything from Bullet is the same except for health
        base.Awake();
        health = 1;
        audio.audioSource = GetComponent<AudioSource>();
        target = GameObject.Find("Protect_target");
    }

    private void Update()
    {
        rbMagnitude = rb.velocity.magnitude;
    }

    void FixedUpdate()
    {
        // get current bullet location and calculate trajectory to target
        Vector2 bullet = transform.localPosition;
        Vector2 bulletDirection = (Vector2) target.transform.localPosition - bullet;

        // apply calculated trajectory to bullet
        rb.velocity = rb.velocity + bulletDirection * Time.deltaTime*1.4f;
        rb.velocity = rb.velocity.normalized * rbMagnitude;
    }

    public override void PlayerHit(Vector2 hitDir)
    {
        //if the player hits the bullet from behind...
        if (Vector2.Angle(hitDir, rb.velocity) > 115)
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
        bSpawner.deadBulletList.Remove(gameObject);
        StartCoroutine(DelayDestruction());
    }

    public override void ReviveProjectile(Vector2 direction, int HP)
    {
        StopAllCoroutines();
        rb.bodyType = RigidbodyType2D.Dynamic;
        audio.PlaySpawnSound();
        particles.EnableLife();
        coll.enabled = true;
        health = 1;
        rb.AddForce(direction * moveSpeed);
        rbMagnitude = rb.velocity.magnitude;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // if bullet collides with environment
        if (collision.gameObject.layer == 8)
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

    IEnumerator DelayDestruction()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    IEnumerator DeactivateCollider()
    {
        coll.enabled = false;
        yield return new WaitForSeconds(0.5f);
        transform.position = Vector2.up * 60;
    }
}
