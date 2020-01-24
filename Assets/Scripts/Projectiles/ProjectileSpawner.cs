using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    //Bullet prefabs
    public GameObject bulletPrefab;
    public GameObject boidBulletPrefab;
    public GameObject bombPrefab;

    //List of all bullets
    [SerializeField]
    private GameObjectSet aliveProjectileSet;
    //List to be object pooled
    [SerializeField]
    private GameObjectSet deadProjectileSet;

    //Actual countdown
    [SerializeField]
    protected float sequenceTimer;
    //Max time until next bullet wave
    private float timerMaxCD = 30.0f;

    public VarInt Score;

    private int maxNumToSpawn;

    private int minNumSequences;       // the minimum # of times the spawner will fire bullets during a wave
    private int maxNumSequences;       // the maximum # of times the spawner will fire bullets during a wave

    private int minNumBulletsToFire;    // the minimum # of bullets that will fire for a shot
    private int maxNumBulletsToFire;    // the maximum # of bullets that will fire for a shot

    private int maxProjectilesAllowed = 5;  // maximum number of bullets allowed on screen at a time

    private int numProjectilesToFire;
    private int numOfSequences;
    private float timeBetweenSequences;

    private int bulletHP;
    private int boidScoreThreshold = 30;

    private void Awake()
    {
        deadProjectileSet.Clear();
        aliveProjectileSet.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        maxProjectilesAllowed = 5;

        minNumSequences = 1;
        maxNumSequences = 4;

        minNumBulletsToFire = 1;
        maxNumBulletsToFire = 3;

        sequenceTimer = 2.5f;
        timerMaxCD = 30.0f;

        bulletHP = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (sequenceTimer <= 0)
        {
            numOfSequences = Random.Range(minNumSequences, maxNumSequences);
            timeBetweenSequences = 0;

            //Reset wave timer
            sequenceTimer = timerMaxCD;
        }
        else
        {
            //Count down to bullet spawn
            sequenceTimer -= Time.deltaTime;
        }

        if (timeBetweenSequences <= 0 && numOfSequences > 0)
        {
            SpawnSequence(Random.Range(0, 3));

            numOfSequences--;
            timeBetweenSequences = Random.Range(1.2f, 2.0f);
        }
        else
        {
            timeBetweenSequences -= Time.deltaTime;
        }
    }

    // Adds bullet to the dead bullet list
    // Increments difficulty
    public void AddBulletToDeadList(GameObject b)
    {
        aliveProjectileSet.Remove(b);
        deadProjectileSet.Add(b);

        // Reduces the wave timer if true
        if (numOfSequences == 0 && aliveProjectileSet.Count() == 0 && sequenceTimer > 2.5f) sequenceTimer = 2.5f;

        // increases bullet spawn health based on point value
        if (Score.value >= 150) bulletHP = 3;
        else if (Score.value >= 50) bulletHP = 2;

        if (Score.value % 10 == 0 && Score.value <= 150)
        {
            maxProjectilesAllowed++;
            maxNumSequences++;
            maxNumBulletsToFire++;
            timerMaxCD--;
        }
    }


    // numForSpawnType: 
    //Random Spawn Pattern - 0
    //Line Spawn Parttern - 1
    //X Spawn Pattern - 2
    void SpawnSequence(int numForSpawnType)
    {
        // Chance to spawn an extra boid bullet
        if (Score.value >= 150 && Random.Range(1, 20) < 5)
        {
            GameObject newBoidBullet = Instantiate(boidBulletPrefab, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f)), Quaternion.identity);
            newBoidBullet.GetComponent<Projectile>().ReviveProjectile(GetRandomShootDirection(), bulletHP);
        }

        int numProjectilesToFire = Random.Range(minNumBulletsToFire, maxNumBulletsToFire);
        switch (numForSpawnType)
        {
            //Spawn Pattern: Random 
            case 0:
                StartCoroutine(SpawnRandom(numProjectilesToFire));
                break;

            //Spawn Pattern: Line
            case 1:
                StartCoroutine(SpawnLine(numProjectilesToFire));
                break;

            //Spawn Pattern: X
            case 2:
                StartCoroutine(SpawnX());
                break;
        }
    }

    // Returns a random direction to shoot a bullet
    Vector2 GetRandomShootDirection()
    {
        float xdir, ydir;
        if (Random.Range(-1, 2) > 0) xdir = 1.0f;
        else xdir = -1.0f;
        if (Random.Range(-1, 2) > 0) ydir = Random.Range(0.75f, 1.5f);
        else ydir = Random.Range(-1.5f, -0.75f);
        return new Vector2(xdir, ydir).normalized;
    }

    // shoots bullets in random directions
    IEnumerator SpawnRandom(int spawnCount)
    {
        int i = 0;
        while (i < spawnCount)
        {
            if (deadProjectileSet.Count() > spawnCount - i && aliveProjectileSet.Count() < maxProjectilesAllowed)
            {
                if (Score.value >= boidScoreThreshold && Random.Range(1, 11) < 3)
                {
                    GameObject newBoidBullet = Instantiate(boidBulletPrefab, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f)), Quaternion.identity);
                    newBoidBullet.GetComponent<Projectile>().ReviveProjectile(GetRandomShootDirection(), bulletHP);
                }
                else
                {
                    deadProjectileSet.items[0].transform.position = new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f));
                    deadProjectileSet.items[0].GetComponent<Projectile>().ReviveProjectile(GetRandomShootDirection(), bulletHP);
                    aliveProjectileSet.Add(deadProjectileSet.items[0]);
                    deadProjectileSet.Remove(deadProjectileSet.items[0]);
                }
            }
            else if (aliveProjectileSet.Count() < maxProjectilesAllowed)
            {
                if (Score.value >= boidScoreThreshold && Random.Range(1, 11) < 3)
                {
                    GameObject newBoidBullet = Instantiate(boidBulletPrefab, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f)), Quaternion.identity);
                    newBoidBullet.GetComponent<Projectile>().ReviveProjectile(GetRandomShootDirection(), bulletHP);
                }
                else
                {
                    GameObject newBullet = Instantiate(bulletPrefab, new Vector2(Random.Range(-8.0f, 8.0f), Random.Range(2.0f, 4.0f)), Quaternion.identity);
                    newBullet.GetComponent<Projectile>().ReviveProjectile(GetRandomShootDirection(), bulletHP);
                    aliveProjectileSet.Add(newBullet);
                }
            }
            yield return new WaitForSeconds(0.2f);
            i++;
        }
    }

    IEnumerator SpawnLine(int spawnCount)
    {
        //if dir = 0 then spawn leftward | if dir = 1 then spawn right
        int dir = Random.Range(0, 2);
        float xMin = -6.0f;
        float xMax = 6.0f;
        if (spawnCount > 14) spawnCount = 14;
        if (spawnCount > 2 && dir == 0)
        {
            xMin = -7.0f + spawnCount;
            xMax = 7.0f;
        }
        else if (spawnCount > 2 && dir == 1)
        {
            xMin = -7.0f;
            xMax = 7.0f - spawnCount;
        }

        //Holds the X pos for spawns that need to spawn near each other
        float randX = Random.Range(xMin, xMax);
        int i = 0;
        int offset = 0;
        Vector2 shootDirection = GetRandomShootDirection();
        while (i < spawnCount)
        {
            if (deadProjectileSet.Count() >= spawnCount - i && aliveProjectileSet.Count() < maxProjectilesAllowed)
            {
                //Use old bullets and remove them from the list
                //Number of balls to spawn
                deadProjectileSet.items[0].transform.position = new Vector2(randX + offset, 4.0f);
                deadProjectileSet.items[0].GetComponent<Projectile>().ReviveProjectile(shootDirection, bulletHP);
                aliveProjectileSet.Add(deadProjectileSet.items[0]);
                deadProjectileSet.Remove(deadProjectileSet.items[0]);
            }
            else if (aliveProjectileSet.Count() < maxProjectilesAllowed)
            {
                //Make new bullets
                GameObject newBullet = Instantiate(bulletPrefab, new Vector2(randX + offset, 4.0f), Quaternion.identity);
                newBullet.GetComponent<Projectile>().ReviveProjectile(shootDirection, bulletHP);
                aliveProjectileSet.Add(newBullet);
            }

            if (dir == 0) offset--;
            else offset++;

            i++;
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator SpawnX()
    {
        Vector2 shootDirection = GetRandomShootDirection();
        Vector2 negShootDir = shootDirection;
        negShootDir.x = -negShootDir.x;
        float randX = Random.Range(-6.0f, 6.0f);
        float offset = 0.3f;
        int i = 0;
        while (i < 3)
        {
            if (deadProjectileSet.Count() > 1 && aliveProjectileSet.Count() < maxProjectilesAllowed - 1)
            {
                // this switch determines which part of X is being shot
                switch (i)
                {
                    // top
                    case 0:
                        // Top Right
                        deadProjectileSet.items[0].transform.position = new Vector2(randX + offset, 3.7f + offset);
                        deadProjectileSet.items[0].GetComponent<Projectile>().ReviveProjectile(shootDirection, bulletHP);
                        aliveProjectileSet.Add(deadProjectileSet.items[0]);
                        // Top Left
                        deadProjectileSet.items[1].transform.position = new Vector2(randX - offset, 3.7f + offset);
                        deadProjectileSet.items[1].GetComponent<Projectile>().ReviveProjectile(negShootDir, bulletHP);
                        aliveProjectileSet.Add(deadProjectileSet.items[1]);
                        break;

                    // middle
                    case 1:
                        // Middle
                        deadProjectileSet.items[0].transform.position = new Vector2(randX, 3.7f);
                        deadProjectileSet.items[0].GetComponent<Projectile>().ReviveProjectile(shootDirection, bulletHP);
                        aliveProjectileSet.Add(deadProjectileSet.items[0]);
                        // neg-Middle
                        deadProjectileSet.items[1].transform.position = new Vector2(randX, 3.7f);
                        deadProjectileSet.items[1].GetComponent<Projectile>().ReviveProjectile(negShootDir, bulletHP);
                        aliveProjectileSet.Add(deadProjectileSet.items[1]);
                        break;


                    // bottom
                    case 2:
                        // Bottom right
                        deadProjectileSet.items[0].transform.position = new Vector2(randX - offset, 3.7f - offset);
                        deadProjectileSet.items[0].GetComponent<Projectile>().ReviveProjectile(shootDirection, bulletHP);
                        aliveProjectileSet.Add(deadProjectileSet.items[0]);
                        // Bottom Left
                        deadProjectileSet.items[1].transform.position = new Vector2(randX + offset, 3.7f - offset);
                        deadProjectileSet.items[1].GetComponent<Projectile>().ReviveProjectile(negShootDir, bulletHP);
                        aliveProjectileSet.Add(deadProjectileSet.items[1]);
                        break;
                }


                deadProjectileSet.Remove(deadProjectileSet.items[1]);
                deadProjectileSet.Remove(deadProjectileSet.items[0]);
            }
            else if (aliveProjectileSet.Count() < maxProjectilesAllowed - 1)
            {

                switch (i)
                {
                    // top
                    case 0:
                        // Top Right
                        GameObject newBullet = Instantiate(bulletPrefab, new Vector2(randX + offset, 3.7f + offset), Quaternion.identity);
                        newBullet.GetComponent<Projectile>().ReviveProjectile(shootDirection, bulletHP);
                        aliveProjectileSet.Add(newBullet);
                        // Top Left
                        newBullet = Instantiate(bulletPrefab, new Vector2(randX - offset, 3.7f + offset), Quaternion.identity);
                        newBullet.GetComponent<Projectile>().ReviveProjectile(negShootDir, bulletHP);
                        aliveProjectileSet.Add(newBullet);
                        break;

                    // middle
                    case 1:
                        // Middle
                        newBullet = Instantiate(bulletPrefab, new Vector2(randX, 3.7f), Quaternion.identity);
                        newBullet.GetComponent<Projectile>().ReviveProjectile(shootDirection, bulletHP);
                        aliveProjectileSet.Add(newBullet);
                        // neg-Middle
                        newBullet = Instantiate(bulletPrefab, new Vector2(randX, 3.7f), Quaternion.identity);
                        newBullet.GetComponent<Projectile>().ReviveProjectile(negShootDir, bulletHP);
                        aliveProjectileSet.Add(newBullet);
                        break;

                    // top
                    case 2:
                        // Bottom Right
                        newBullet = Instantiate(bulletPrefab, new Vector2(randX - offset, 3.7f - offset), Quaternion.identity);
                        newBullet.GetComponent<Projectile>().ReviveProjectile(shootDirection, bulletHP);
                        aliveProjectileSet.Add(newBullet);
                        // Bottom Left
                        newBullet = Instantiate(bulletPrefab, new Vector2(randX + offset, 3.7f - offset), Quaternion.identity);
                        newBullet.GetComponent<Projectile>().ReviveProjectile(negShootDir, bulletHP);
                        aliveProjectileSet.Add(newBullet);
                        break;
                }
            }
            i++;
            yield return new WaitForSeconds(0.175f);
        }
    }
}
