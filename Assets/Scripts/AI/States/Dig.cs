using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dig : State
{
    private bool doneOnce;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        //Timer based on the atention span
        if (manager.currentTime < manager.stats.atention * manager.maxTime)
        {
            manager.currentTime += Time.deltaTime;

            if (!doneOnce)
            {
                //Need to play anim
                //Need to add icon 
                //Need to play sound

                manager.agent.isStopped = true;
                manager.agent.velocity = Vector3.zero;

                Debug.Log("Digging");

                doneOnce = true;

            }

            return this;
        }

        //If time ran out, than return explore state
        else
        {
            manager.currentTime = 0.0f;
            manager.agent.isStopped = false;

            doneOnce = false;

            manager.petMenu.actionIcon.texture = manager.petMenu.exploreIcon;

            Debug.Log("Exit digging");

            return manager.exploreState;
        }
    }
}

