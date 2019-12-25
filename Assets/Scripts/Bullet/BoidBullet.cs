using UnityEngine;
using System.Collections;

public class BoidBullet : Bullet
{
    [SerializeField]
    private GameObject target;

    // Start is called before the first frame update
    override protected void Awake()
    {
        // everything from Bullet is the same except for health
        base.Awake();
        health = 1;
        target = GameObject.Find("Protect_target");
    }

    override protected void Update()
    {
        base.Update();
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

    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (health <= 0)
        {
            bSpawner.deadBulletList.Remove(gameObject);
            StartCoroutine(DelayDestruction());
        }
    }

    public override void ReviveBullet(Vector2 direction, int HP)
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

    IEnumerator DelayDestruction()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
