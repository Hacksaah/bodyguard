using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPoolHandler : MonoBehaviour 
{
    //Projectile prefabs
    public GameObjectSet prefabSet;
        // Help
        //  0. bullet prefab
        //  1. boid bullet prefab
        //  2. bomb prefab

    //List of all bullets
    [SerializeField]
    private GameObjectSet aliveProjectileSet;
    //List to be object pooled
    [SerializeField]
    private GameObjectSet deadProjectileSet;

    public VarInt maxProjectilesAllowed;

    public void SetDefaultValues()
    {
        deadProjectileSet.Clear();
        aliveProjectileSet.Clear();
    }


    public void CheckDeadProjectileSetForSequence(int amount, int index)
    {
        // if not enough projectiles, then make new ones
        if(deadProjectileSet.Count() < amount)
        {
            int diff = amount - deadProjectileSet.Count();
            for(int i = 0; i < diff; i++)
            {
                GameObject newProjectile = Instantiate(prefabSet.items[index]);
                newProjectile.transform.position = Vector2.up * 60;
                deadProjectileSet.Add(newProjectile);
            }
        }
    }
}
