using UnityEngine;

public class BoidBullet : Bullet
{
    // Start is called before the first frame update
    new void Start()
    {
        // everything from Bullet is the same except for health
        base.Start();
        health = 1;
    }

    new void Update()
    {
        base.Update();
    }

    new void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
