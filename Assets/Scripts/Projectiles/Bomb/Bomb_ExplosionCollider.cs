using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_ExplosionCollider : MonoBehaviour
{
    private CircleCollider2D col;
    private bool isFriendly;

    private void Awake()
    {
        col = GetComponent<CircleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {        
        gameObject.SetActive(false);
    }

    public void ActivateCollider(bool _isFriendly)
    {
        isFriendly = _isFriendly;
        StartCoroutine(DecreaseHitBoxSize());
    }

    IEnumerator DecreaseHitBoxSize()
    {
        col.radius = 2.45f;
        yield return new WaitForSeconds(0.25f);
        while(col.radius >= 0.65f)
        {
            col.radius += 0.12f * (0.5f - col.radius);
            yield return null;
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Something entered :: " + collision.name);
        if (isFriendly && collision.gameObject.layer == 11)
        {

            Vector2 hitDir = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<Projectile>().PlayerHit(hitDir);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("Something here :: " + collision.name);
        if (isFriendly && collision.gameObject.layer == 11)
        {
            
            Vector2 hitDir = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<Projectile>().PlayerHit(hitDir);
        }
    }   
    
}
