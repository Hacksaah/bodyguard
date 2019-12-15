using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* This Manager...
 * 
 * Creates a whole slew of bullets
 * Tracks how many bullets are on screen
 * Spawns bullets 
 * 
 */
public class Bullet_Manager : MonoBehaviour
{
    public GameObject BulletPrefab;
    public GameObject BoidBulletPrefab;

    protected List<GameObject> bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        MakeBullets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MakeBullets()
    {
        if (BulletPrefab == null || BoidBulletPrefab == null)
            Debug.Log("WARNING :: COULD NOT MAKE BULLETS; Bullet_Manager doesn't have a bullet prefab");
        else
        {
            for(int i = 0; i < 18; i++)
            {
                GameObject newBullet = Instantiate(BulletPrefab);
                newBullet.SetActive(false);
                bullets.Add(newBullet);
            }
        }
    }

    
}
