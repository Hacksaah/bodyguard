using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    //Bullet prefab
    public GameObject bulletPrefab;
    public GameObject boidBulletPrefab;

    //List of all bullets
    [SerializeField]
    protected List<GameObject> aliveBulletList;
    //List to be object pooled
    [SerializeField]
    public List<GameObject> deadBulletList;
    //Max time until next bullet wave
    private float timerMaxCD = 30.0f;
    //Actual countdown
    [SerializeField]
    protected float timerCD;

    public VarInt Score;

    private int maxNumToSpawn;

    private int minNumShotsFired;       // the minimum # of times the spawner will fire bullets during a wave
    private int maxNumShotsFired;       // the maximum # of times the spawner will fire bullets during a wave

    private int minNumBulletsToFire;    // the minimum # of bullets that will fire for a shot
    private int maxNumBulletsToFire;    // the maximum # of bullets that will fire for a shot

    private int maxBulletsAllowed = 5;  // maximum number of bullets allowed on screen at a time

    private int numBulletsToFire;
    private int numShotsToFire;
    private float timeBetweenShots;

    private int bulletHP;
    private int boidScoreThreshold = 30;

    // Start is called before the first frame update
    void Start()
    {
        maxBulletsAllowed = 5;

        minNumShotsFired = 1;
        maxNumShotsFired = 4;

        minNumBulletsToFire = 1;
        maxNumBulletsToFire = 3;

        timerCD = 2.5f;

        bulletHP = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerCD <= 0)
        {
            numShotsToFire = Random.Range(minNumShotsFired, maxNumShotsFired);
            timeBetweenShots = 0;

            //Reset timer
            timerCD = timerMaxCD;
        }
        else
        {
            //Count down to bullet spawn
            timerCD -= Time.deltaTime;
        }

        if(timeBetweenShots <= 0 && numShotsToFire > 0)
        {
            SpawnBullet(Random.Range(0, 3));

            numShotsToFire--;
            timeBetweenShots = Random.Range(1.2f, 2.0f);
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }        
    }

    //Adds bullet to the dead bullet list
    public void AddBulletToDeadList(GameObject b)
    {
        aliveBulletList.Remove(b);
        deadBulletList.Add(b);
        if (numShotsToFire == 0 && aliveBulletList.Count == 0 && timerCD > 2.5f) timerCD = 2.5f;
        if (Score.value == 10) bulletHP++;
        if (Score.value == 15) bulletHP++;
    }


    // numForSpawnType: 
        //Random Spawn Pattern - 0
        //Line Spawn Parttern - 1
        //X Spawn Pattern - 2
    void SpawnBullet(int numForSpawnType)
    {
        // Chance to spawn an extra boid bullet
        if(Score.value >= 150 &&  Random.Range(1, 20) < 5)
        {
            GameObject newBoidBullet = Instantiate(boidBulletPrefab, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f)), Quaternion.identity);
            newBoidBullet.GetComponent<BoidBullet>().ReviveBullet(GetRandomShootDirection(), bulletHP);
        }

        int numBulletsToFire = Random.Range(minNumBulletsToFire, maxNumBulletsToFire);
        switch (numForSpawnType)
        {            
            //Spawn Pattern: Random 
            case 0:
                StartCoroutine(SpawnRandom(numBulletsToFire));
                break;
                
            //Spawn Pattern: Line
            case 1:
                StartCoroutine(SpawnLine(numBulletsToFire));
                break;

            //Spawn Pattern: X
            case 2:
                Vector2 shootDirection = GetRandomShootDirection();
                float randX = Random.Range(-6.0f, 6.0f);
                float offset = 0.3f;
                if (deadBulletList.Count >= 5)
                {
                    //Middle
                    deadBulletList[0].transform.position = new Vector2(randX, 4.0f);
                    deadBulletList[0].GetComponent<Bullet>().ReviveBullet(shootDirection, bulletHP);
                    aliveBulletList.Add(deadBulletList[0]);
                    
                    //Top Right
                    deadBulletList[1].transform.position = new Vector2(randX + offset, 4.0f + offset);
                    deadBulletList[1].GetComponent<Bullet>().ReviveBullet(shootDirection, bulletHP);
                    aliveBulletList.Add(deadBulletList[1]);
                    
                    //Top Left
                    deadBulletList[2].transform.position = new Vector2(randX - offset, 4.0f + offset);
                    deadBulletList[2].GetComponent<Bullet>().ReviveBullet(-shootDirection, bulletHP);
                    aliveBulletList.Add(deadBulletList[2]);
                    
                    //Bottom Right
                    deadBulletList[3].transform.position = new Vector2(randX - offset, 4.0f - offset);
                    deadBulletList[3].GetComponent<Bullet>().ReviveBullet(shootDirection, bulletHP);
                    aliveBulletList.Add(deadBulletList[3]);
                    
                    //Bottom Left
                    deadBulletList[4].transform.position = new Vector2(randX + offset, 4.0f - offset);
                    deadBulletList[4].GetComponent<Bullet>().ReviveBullet(-shootDirection, bulletHP);
                    aliveBulletList.Add(deadBulletList[4]);

                    deadBulletList.Remove(deadBulletList[4]);
                    deadBulletList.Remove(deadBulletList[3]);
                    deadBulletList.Remove(deadBulletList[2]);
                    deadBulletList.Remove(deadBulletList[1]);
                    deadBulletList.Remove(deadBulletList[0]);
                }
                else
                {
                    //Middle
                    GameObject newBullet = Instantiate(bulletPrefab, new Vector2(randX, 4.0f), Quaternion.identity);
                    newBullet.GetComponent<Bullet>().ReviveBullet(shootDirection, bulletHP);
                    aliveBulletList.Add(newBullet);
                    //Top Right
                    newBullet = Instantiate(bulletPrefab, new Vector2(randX + offset, 4.0f + offset), Quaternion.identity);
                    newBullet.GetComponent<Bullet>().ReviveBullet(shootDirection, bulletHP);
                    aliveBulletList.Add(newBullet);
                    //Top Left
                    newBullet = Instantiate(bulletPrefab, new Vector2(randX - offset, 4.0f + offset), Quaternion.identity);
                    newBullet.GetComponent<Bullet>().ReviveBullet(-shootDirection, bulletHP);
                    aliveBulletList.Add(newBullet);
                    //Bottom Right
                    newBullet = Instantiate(bulletPrefab, new Vector2(randX - offset, 4.0f - offset), Quaternion.identity);
                    newBullet.GetComponent<Bullet>().ReviveBullet(shootDirection, bulletHP);
                    aliveBulletList.Add(newBullet);
                    //Bottom Left
                    newBullet = Instantiate(bulletPrefab, new Vector2(randX + offset, 4.0f - offset), Quaternion.identity);
                    newBullet.GetComponent<Bullet>().ReviveBullet(-shootDirection, bulletHP);
                    aliveBulletList.Add(newBullet);
                }
                break;
        }
    }

    Vector2 GetRandomShootDirection()
    {
        float xdir, ydir;        
        if (Random.Range(-1, 2) > 0) xdir = 1.0f;
        else xdir = -1.0f;
        if(Random.Range(-1, 2) > 0) ydir = Random.Range(0.75f, 1.5f);
        else ydir = Random.Range(-1.5f, -0.75f);
        return new Vector2(xdir, ydir).normalized;
    }

    IEnumerator SpawnRandom(int spawnCount)
    {
        int i = 0;
        while (i < spawnCount)
        {
            if (deadBulletList.Count > spawnCount-i)
            {
                if (Score.value >= boidScoreThreshold && Random.Range(1, 11) < 3)
                {
                    GameObject newBoidBullet = Instantiate(boidBulletPrefab, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f)), Quaternion.identity);
                    newBoidBullet.GetComponent<Bullet>().ReviveBullet(GetRandomShootDirection(), bulletHP);
                }
                else
                {
                    deadBulletList[0].transform.position = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f));
                    deadBulletList[0].GetComponent<Bullet>().ReviveBullet(GetRandomShootDirection(), bulletHP);
                    aliveBulletList.Add(deadBulletList[0]);
                    deadBulletList.Remove(deadBulletList[0]);
                }
            }
            else
            {
                if (Random.Range(1, 11) < 5)
                {
                    GameObject newBoidBullet = Instantiate(boidBulletPrefab, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f)), Quaternion.identity);
                    newBoidBullet.GetComponent<Bullet>().ReviveBullet(GetRandomShootDirection(), bulletHP);
                }
                else
                {
                    GameObject newBullet = Instantiate(bulletPrefab, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f)), Quaternion.identity);
                    newBullet.GetComponent<Bullet>().ReviveBullet(GetRandomShootDirection(), bulletHP);
                    aliveBulletList.Add(newBullet);
                }
            }
            yield return new WaitForSeconds(0.2f);
            i++;
        }
    }

    IEnumerator SpawnLine(int spawnCount)
    {
        //Holds the X pos for spawns that need to spawn near each other
        float randX = Random.Range(-6.0f, 6.0f);
        int i = 0;
        Vector2 shootDirection = GetRandomShootDirection();
        while (i < spawnCount)
        {
            if (deadBulletList.Count >= spawnCount-i)
            {
                //Use old bullets and remove them from the list
                //Number of balls to spawn
                deadBulletList[0].transform.position = new Vector2(randX + i, 4.0f);
                deadBulletList[0].GetComponent<Bullet>().ReviveBullet(shootDirection, bulletHP);
                aliveBulletList.Add(deadBulletList[0]);
                deadBulletList.Remove(deadBulletList[0]);
            }
            else
            {
                //Make new bullets
                GameObject newBullet = Instantiate(bulletPrefab, new Vector2(randX + i, 4.0f), Quaternion.identity);
                newBullet.GetComponent<Bullet>().ReviveBullet(shootDirection, bulletHP);
                aliveBulletList.Add(newBullet);                
            }
            i++;
            yield return new WaitForSeconds(0.2f);
        }
    }


}
