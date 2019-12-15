using UnityEngine;

public class BoidBullet : Bullet
{
    [SerializeField]
    private GameObject target;

    // Start is called before the first frame update
    override protected void Start()
    {
        // everything from Bullet is the same except for health
        base.Start();
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
        rb.velocity = rb.velocity + bulletDirection * Time.deltaTime;
        rb.velocity = rb.velocity.normalized * rbMagnitude;
    }

    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
