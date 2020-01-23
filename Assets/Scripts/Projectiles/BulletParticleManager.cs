using UnityEngine;

public class BulletParticleManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Trail;
    [SerializeField]
    private GameObject Burst;
    [SerializeField]
    private GameObject Death;

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

    //Plays Burst particle
    public void EnableBurst()
    {
        ps_burst.Play();        
    }

    //Plays death particle
    public void EnableDeath()
    {
        ps_death.Play();
        
        ps_trail.Pause();
        ps_trail.Clear();
    }

    //Reenable bullet particles -- for bullet spawner
    public void EnableLife()
    {
        ps_trail.Play();
    }

    //Changes the starting color for each particle system
    public void ChangeParticleColor(int index)
    {
        Color col = Game_Manager.instance.GetBulletColor(index);

        var main = ps_trail.main;
        main.startColor = col;

        main = ps_burst.main;
        main.startColor = col;

        main = ps_death.main;
        main.startColor = col;
    }
}
