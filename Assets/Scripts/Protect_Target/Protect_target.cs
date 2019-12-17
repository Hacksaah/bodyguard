using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Protect_target : MonoBehaviour
{
    public float moveSpeed = 35;

    protected Rigidbody2D rb;

    public GameEvent GameOverEvent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(Move());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Move()
    {
        float time = Random.Range(.5f, 1.5f);
        int dx = Random.Range(-1, 1);
        if (dx == 0)
            dx++;
        while(time > 0)
        {
            rb.AddForce(Vector2.right * dx * moveSpeed);
            time -= Time.deltaTime;
            yield return null;
        }
        if(time <= 0)
            StartCoroutine(Move());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //If a projectile collides with the protect target the game ends
        if (collision.gameObject.layer == 11)
        {
            collision.gameObject.GetComponent<Bullet>().audio.PlayDeathSound();
            GameOverEvent.Raise();
        }
    }
}
