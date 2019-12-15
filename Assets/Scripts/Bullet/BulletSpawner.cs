using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    //Bullet prefab
    [SerializeField]
    protected GameObject bulletPrefab;
    //List of all bullets
    [SerializeField]
    protected List<GameObject> aliveBulletList;
    //List to be object pooled
    [SerializeField]
    protected List<GameObject> deadBulletList;
    //Max time until bullet spawns
    [SerializeField]
    protected float timerMaxCD;
    //Actual countdown
    [SerializeField]
    protected float timerCD;
    //Number of bullets to spawn for the line and random spawning patterns
    [SerializeField]
    protected int numToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (timerCD <= 0)
        {
            //Spawn bullet - Random number to decide what kind of spawning
            SpawnBullet(Random.Range(0, 3));
            //Reset timer
            timerCD = timerMaxCD;            
        }
        else
        {
            //Count down to bullet spawn
            timerCD -= Time.deltaTime;
        }
    }

    //Adds bullet to the dead bullet list
    public void AddBulletToDeadList(GameObject b)
    {
        aliveBulletList.Remove(b);
        deadBulletList.Add(b);
    }

    //numForSpawnType: 
    //Random Spawn Pattern - 0
    //Line Spawn Parttern - 1
    //X Spawn Pattern - 2
    void SpawnBullet(int numForSpawnType)
    {
       
        //Holds the X pos for spawns that need to spawn near each other
        float randX = Random.Range(-6.0f, 6.0f);

        switch (numForSpawnType)
        {
            //Spawn Pattern: Random 
            case 0:
                if (deadBulletList.Count > numToSpawn)
                {
                    //Use old bullets and remove them from the list
                    //Number of balls to spawn
                    for (int i = 0; i < numToSpawn; i++)
                    {
                        deadBulletList[0].transform.position = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f));
                        deadBulletList[0].GetComponent<Bullet>().ReviveBullet();
                        aliveBulletList.Add(deadBulletList[0]);
                        deadBulletList.Remove(deadBulletList[0]);
                    }
                }
                else
                {
                    //Make new bullet
                    for (int i = 0; i < numToSpawn; i++)
                    {
                        GameObject newBullet = Instantiate(bulletPrefab, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f)), Quaternion.identity);
                        aliveBulletList.Add(newBullet);
                    }
                }
                break;

            //Spawn Pattern: Line
            case 1:
                
                if (deadBulletList.Count >= numToSpawn)
                {
                    //Use old bullets and remove them from the list
                    //Number of balls to spawn
                    for (int i = 0; i < numToSpawn; i++)
                    {
                        deadBulletList[0].transform.position = new Vector2(randX + i, 4.0f);
                        deadBulletList[0].GetComponent<Bullet>().ReviveBullet();
                        aliveBulletList.Add(deadBulletList[0]);
                        deadBulletList.Remove(deadBulletList[0]);
                    }
                }
                else
                {
                    //Make new bullets
                    for (int i = 0; i < numToSpawn; i++)
                    {
                        GameObject newBullet = Instantiate(bulletPrefab, new Vector2(randX + i, 4.0f), Quaternion.identity);
                        aliveBulletList.Add(newBullet);
                    }
                }
                break;

            //Spawn Pattern: X
            case 2:
                float offset = 0.3f;
                if (deadBulletList.Count >= 5)
                {
                    //Middle
                    deadBulletList[0].transform.position = new Vector2(randX, 4.0f);
                    deadBulletList[0].GetComponent<Bullet>().ReviveBullet();
                    aliveBulletList.Add(deadBulletList[0]);
                    deadBulletList.Remove(deadBulletList[0]);
                    //Top Right
                    deadBulletList[1].transform.position = new Vector2(randX + offset, 4.0f + offset);
                    deadBulletList[1].GetComponent<Bullet>().ReviveBullet();
                    aliveBulletList.Add(deadBulletList[1]);
                    deadBulletList.Remove(deadBulletList[1]);
                    //Top Left
                    deadBulletList[2].transform.position = new Vector2(randX - offset, 4.0f + offset);
                    deadBulletList[2].GetComponent<Bullet>().ReviveBullet();
                    aliveBulletList.Add(deadBulletList[2]);
                    deadBulletList.Remove(deadBulletList[2]);
                    //Bottom Right
                    deadBulletList[3].transform.position = new Vector2(randX - offset, 4.0f - offset);
                    deadBulletList[3].GetComponent<Bullet>().ReviveBullet();
                    aliveBulletList.Add(deadBulletList[3]);
                    deadBulletList.Remove(deadBulletList[3]);
                    //Bottom Left
                    deadBulletList[4].transform.position = new Vector2(randX + offset, 4.0f - offset);
                    deadBulletList[4].GetComponent<Bullet>().ReviveBullet();
                    aliveBulletList.Add(deadBulletList[4]);
                    deadBulletList.Remove(deadBulletList[4]);
                }
                else
                {
                    //Middle
                    GameObject newBullet = Instantiate(bulletPrefab, new Vector2(randX, 4.0f), Quaternion.identity);
                    aliveBulletList.Add(newBullet);
                    //Top Right
                    newBullet = Instantiate(bulletPrefab, new Vector2(randX + offset, 4.0f + offset), Quaternion.identity);
                    aliveBulletList.Add(newBullet);
                    //Top Left
                    newBullet = Instantiate(bulletPrefab, new Vector2(randX - offset, 4.0f + offset), Quaternion.identity);
                    aliveBulletList.Add(newBullet);
                    //Bottom Right
                    newBullet = Instantiate(bulletPrefab, new Vector2(randX - offset, 4.0f - offset), Quaternion.identity);
                    aliveBulletList.Add(newBullet);
                    //Bottom Left
                    newBullet = Instantiate(bulletPrefab, new Vector2(randX + offset, 4.0f - offset), Quaternion.identity);
                    aliveBulletList.Add(newBullet);
                }
                break;
        }
                
    }
}
