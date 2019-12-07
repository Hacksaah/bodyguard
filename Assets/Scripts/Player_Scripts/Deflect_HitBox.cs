using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deflect_HitBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "projectile")
        {
            Debug.Log("Projectile triggered");
            Rigidbody2D proj_rb = collision.GetComponent<Rigidbody2D>();
            Vector2 vel = proj_rb.velocity;
            vel.x = -vel.x;
            proj_rb.velocity = vel;
        }
    }
}
