using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject explosionCol;

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
        StartCoroutine(SetFuse());
    }

    // Update is called once per frame
    void Update() 
    {
        
    }

    //Bombs have a ticking mechanic
    IEnumerator SetFuse()
    {
        yield return new WaitForSeconds(fuseTime);
        DetonateBomb();
    }

    //Bombs explode
    void DetonateBomb()
    {
        Debug.Log("Detonate bomb");
        explosionCol.SetActive(true);
    }

    //Bombs can also destroy bullets if deflected

}
