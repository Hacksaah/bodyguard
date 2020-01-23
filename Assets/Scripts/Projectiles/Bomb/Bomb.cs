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

    // Update is called once per frame
    void Update() 
    {
        
    }

    override public void PlayerHit(Vector2 hitDir)
    {
        colorLerp.StopAllCoroutines();
        isFriendly = true;
        rb.velocity.Set(rb.velocity.x, 0);
        rb.AddForce(-hitDir * hitForce + Vector2.up * 250);
        GetComponent<SpriteRenderer>().color = playerColor.value;
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
        explosionCol.GetComponent<Bomb_ExplosionCollider>().ActivateCollider(isFriendly);
    }    
}
