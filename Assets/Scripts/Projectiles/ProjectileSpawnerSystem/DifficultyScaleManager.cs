using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScaleManager
{
    public VarInt maxProjectilesAllowed; // maximum number of bullets allowed on screen at a time

    public int minNumSequences;       // the minimum # of times the spawner will fire bullets during a wave
    public int maxNumSequences;       // the maximum # of times the spawner will fire bullets during a wave

    public int minNumProjectilesToFire;    // the minimum # of bullets that will fire for a shot
    public int maxNumProjectilesToFire;    // the maximum # of bullets that will fire for a shot

    public int boidScoreThreshold = 30;
    public int bulletHP;

    public float timerMaxCD = 30.0f;

    public VarInt Score;

    public void SetDefaultSettings()
    {
        maxProjectilesAllowed.value = 5;

        minNumSequences = 1;
        maxNumSequences = 4;

        minNumProjectilesToFire = 1;
        maxNumProjectilesToFire = 3;

        bulletHP = 1;

        timerMaxCD = 30.0f;
    }

    //Based on the current score value, this function will alter it's values accordingly
    public void CheckDifficultyStats()
    {
        // increases bullet spawn health based on point value
        if (Score.value >= 150) bulletHP = 3;
        else if (Score.value >= 50) bulletHP = 2;

        if (Score.value % 10 == 0 && Score.value <= 150)
        {
            maxProjectilesAllowed.value++;
            maxNumSequences++;
            maxNumProjectilesToFire++;
            timerMaxCD--;
        }
    }

    public int ReturnRandomNumSequences()
    {
        return Random.Range(minNumSequences, maxNumSequences);
    }

    public int ReturnRandomNumToFire()
    {
        return Random.Range(minNumProjectilesToFire, maxNumProjectilesToFire);
    }

    public float ReturnRandomTimeBetweenSequences()
    {
        return Random.Range(1.2f, 2.0f);
    }

    public bool ChanceForRandomBoidBullet()
    {
        if (Score.value >= boidScoreThreshold && Random.Range(1, 11) < 3) return true;
        return false;
    }
}
