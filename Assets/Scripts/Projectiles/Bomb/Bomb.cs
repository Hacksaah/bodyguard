using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public VarColor playerColor;

    public GameObject explosionCol;
    private ColorLerper colorLerp;

    private Rigidbody2D rb;
    private CircleCollider2D col;

    public float fuseTime = 0.5f;
    private float fuseTimer;

    private void Awake()
    {
        
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

    public void PlayerHit()
    {
        colorLerp.StopAllCoroutines();
        GetComponent<SpriteRenderer>().color = playerColor.value;
    }
}
