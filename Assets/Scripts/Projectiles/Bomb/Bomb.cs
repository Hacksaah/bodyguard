using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Bullet
{
    public int hitForce = 200;
    public VarColor playerColor;

    public GameObject explosionCol;
    private ColorLerper colorLerp;

    private Rigidbody2D rb;
    private CircleCollider2D col;

    public float fuseTime = 0.5f;
    private float fuseTimer;

    private void Awake()
    {
        base.Awake();
        health = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        colorLerp = GetComponent<ColorLerper>();
        StartCoroutine(SetFuse());
    }

    // Update is called once per frame
    void Update() 
    {
        
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
        
        explosionCol.SetActive(true);
    }

    override public void PlayerHit(Vector2 hitDir)
    {
        colorLerp.StopAllCoroutines();
        explosionCol.GetComponent<Bomb_ExplosionCollider>().isFriendly = true;
        rb.velocity.Set(rb.velocity.x, 0);
        rb.AddForce(hitDir * hitForce);
        GetComponent<SpriteRenderer>().color = playerColor.value;
    }
}
