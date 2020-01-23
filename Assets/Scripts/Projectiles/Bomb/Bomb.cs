using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    public int hitForce = 200;
    public VarColor playerColor;

    public GameObject explosionCol;

    private ColorLerper colorLerp;

    public float fuseTime = 0.5f;
    private float fuseTimer;

    override protected void Awake()
    {
        base.Awake();
        health = 1;
        colorLerp = GetComponent<ColorLerper>();
    }

    // Start is called before the first frame update
    void Start()
    {       
        StartCoroutine(SetFuse());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if friendly and collides with a projectile
        if(isFriendly && collision.gameObject.layer == 11)
        {
            StopAllCoroutines();
            DetonateBomb();
        }
    }

    override public void PlayerHit(Vector2 hitDir)
    {
        colorLerp.StopAllCoroutines();
        isFriendly = true;
        gameObject.layer = 10;
        rb.velocity.Set(rb.velocity.x, 0);
        if (hitDir.y < 0) hitDir.y = 0;
        rb.AddForce(hitDir * hitForce + Vector2.up * 250);
        GetComponent<SpriteRenderer>().color = playerColor.value;
    }

    public override void ReviveProjectile(Vector2 direction, int HP)
    {
        StopAllCoroutines();
        health = HP;
        isFriendly = false;
        gameObject.layer = 11;        
        coll.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.AddForce(direction);
        colorLerp.EnableTwoColorLerp();
        StartCoroutine(SetFuse());
    }

    //Bombs have a ticking mechanic
    IEnumerator SetFuse()
    {
        fuseTimer = fuseTime;
        while(fuseTimer >= 0)
        {
            if (fuseTimer > 1) colorLerp.speed = 2f;
            else if (fuseTimer < 0.5) colorLerp.speed = 9f;
            fuseTimer -= Time.deltaTime;
            yield return null;
        }
        DetonateBomb();
    }

    //Bombs explode
    void DetonateBomb()
    {
        // enable explosion collider
        explosionCol.SetActive(true);
        explosionCol.GetComponent<Bomb_ExplosionCollider>().ActivateCollider(isFriendly);
        DisableBomb();
    }

    //code that turns off bomb visuals and interactions
    private void DisableBomb()
    {
        //makes the bomb sprite invisible
        Color tmp = GetComponent<SpriteRenderer>().color;
        tmp.a = 0;
        GetComponent<SpriteRenderer>().color = tmp;

        coll.enabled = false;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
