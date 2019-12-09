﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticleManager : MonoBehaviour
{
    public GameObject Trail;
    public GameObject Burst;
    public GameObject Death;

    private ParticleSystem ps_burst;
    private ParticleSystem ps_trail;
    private ParticleSystem ps_death;

    private void Awake()
    {
        ps_trail = Trail.GetComponent<ParticleSystem>();
        ps_burst = Burst.GetComponent<ParticleSystem>();
        ps_burst.Pause();

        ps_death = Death.GetComponent<ParticleSystem>();
        ps_death.Pause();
    }

    public void EnableBurst()
    {
        ps_burst.Clear();
        ps_burst.Play();        
    }

    public void EnableDeath()
    {
        ps_death.Clear();
        ps_death.Play();
    }
}
