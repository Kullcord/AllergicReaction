using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private bool doneOnce = false;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        //If the probability check is passed, then return rest state,
        //else continue idle state
        if (ProbabilityCheck(stats))
        {
            //return rest state
            manager.currentTime = 0.0f;

            doneOnce = false;

            return manager.restState;
        } 

        if(manager.currentTime < (manager.stats.atention * manager.maxTime) / 2)
        {
            //Play idle animation

            manager.currentTime += Time.deltaTime;

            return this;
        } 
        else
        {
            manager.currentTime = 0.0f;

            doneOnce = false;

            return manager.exploreState;
        }
    }

    private bool ProbabilityCheck(CharacterStats stats)
    {
        if (!doneOnce)
        {
            float rnd = Random.Range(5, 51);

            float percentage = stats.energy * 10f;

            //Debug.Log("rnd is: " + rnd);
            //Debug.Log("percentage: " + percentage);

            if (rnd <= percentage)
                return true;

            doneOnce = true;
        }
        
        return false;
    }
}
