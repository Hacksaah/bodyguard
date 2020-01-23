using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflection_Hitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If collision is a projectile...
        if(collision.gameObject.layer == 11)
        {
            Vector2 hitDir = transform.position - collision.transform.position;
            collision.GetComponent<Projectile>().PlayerHit(hitDir);
        }
    }
}
