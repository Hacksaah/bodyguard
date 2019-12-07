using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 5;

    public Vector2 moveDir = Vector2.one / 2;
    protected Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.Euler(0, 0, 45);
        rb.AddForce(moveDir * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        //rb.MovePosition((Vector2)transform.position + (moveDir * moveSpeed * Time.deltaTime));
    }


    public void UpdateAngle(float newAngle)
    {
        Quaternion curRotation = transform.rotation;
        Vector3 eulerAngles = curRotation.eulerAngles;
        eulerAngles.z = newAngle;
        curRotation.eulerAngles = eulerAngles;
        transform.rotation = curRotation;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        // handle environment collisions
        if (collision.gameObject.layer == 8)
        {
            float vX = rb.velocity.x;
            float vY = rb.velocity.y;
            //Debug.Log(vY);
            Vector3 eulerAngles = transform.rotation.eulerAngles;
            Vector2 velocity = rb.velocity;
            if (vX > 0)
            {
                if (vY < 0)
                {
                    //CalculateCollisionAngle(collision.GetContact(0).point);
                    float temp = moveDir.y;                    
                    temp++;
                    moveDir.y = temp;
                    UpdateAngle(eulerAngles.z + 90);
                }                    
                else
                {
                    //CalculateCollisionAngle(collision.GetContact(0).point);
                    moveDir.y -= 1f;
                    UpdateAngle(eulerAngles.z - 90);
                }
                    
            }
            else
            {
                if (vY > 0)
                    UpdateAngle(eulerAngles.z + 90);
                else
                    UpdateAngle(eulerAngles.z - 90);
            }
        }
    }
    
    void CalculateCollisionAngle(Vector2 collisionPoint)
    {
        // layermask = 8 because ( 8 ) is the layer for environment
        // layermask =  ~layermask inverts layermask and now ignores all other layers
        int ignoreLayers = 8;
        ignoreLayers = ~ignoreLayers;

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, rb.velocity, Mathf.Infinity, ignoreLayers);

        Debug.Log("Collision point " + collisionPoint);
        Debug.Log("Raycast hit " + hit.point);

        float colX = hit.point.x - collisionPoint.x;
        float colY = hit.point.y - collisionPoint.y;
        float mag = Mathf.Sqrt((colX * colX + colY * colY));
        float angle = Mathf.Acos(colY / mag) * 180 / Mathf.PI;

        Debug.Log(angle);
    }
}
