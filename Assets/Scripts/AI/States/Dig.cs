using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dig : State
{
    private bool doneOnce;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        if (manager.currentTime < manager.stats.atention * manager.maxTime)
        {
            manager.currentTime += Time.deltaTime;

            if (!doneOnce)
            {
                //Need to play anim
                //Need to add icon 
                //Need to play sound

                manager.agent.isStopped = true;

                Debug.Log("Digging");

                doneOnce = true;

            }

            return this;
        }
        else
        {
            manager.currentTime = 0.0f;
            manager.agent.isStopped = false;

            doneOnce = false;

            return manager.exploreState;
        }
    }
}

