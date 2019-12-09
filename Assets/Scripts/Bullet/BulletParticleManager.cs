using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticleManager : MonoBehaviour
{
    public GameObject Trail;
    public GameObject Burst;

    private ParticleSystem ps_burst;
    private ParticleSystem ps_trail;

    private void Start()
    {
        ps_trail = Trail.GetComponent<ParticleSystem>();
        ps_burst = Burst.GetComponent<ParticleSystem>();
        ps_burst.Clear();
    }

    public void EnableBurst()
    {
        ps_burst.Clear();
        ps_burst.Play();        
    }
}
