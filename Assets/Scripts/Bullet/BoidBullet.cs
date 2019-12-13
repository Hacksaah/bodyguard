using UnityEngine;

public class BoidBullet : Bullet
{
    // Start is called before the first frame update
    override protected void Start()
    {
        // everything from Bullet is the same except for health
        base.Start();
        health = 1;
    }

    override protected void Update()
    {
        base.Update();
    }

    override protected void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
