using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playing : State
{
    private bool doneOnce = false;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        if (manager.currentTime < manager.stats.atention * manager.maxTime)
        {
            manager.currentTime += Time.deltaTime;//maybe can also base it on the remaining energy level

            if (!doneOnce)
            {
                /*Start animation(maybe if there are more animations can play a random one)
                 * Add icon
                 * Play sound
                 */

                manager.agent.isStopped = true;
                manager.agent.velocity = Vector3.zero;

                Debug.Log("playing");

                doneOnce = true;

            }

            return this;
        } 
        else
        {
            doneOnce = false;

            manager.agent.isStopped = false;

            manager.currentTime = 0.0f;

            manager.petMenu.actionIcon.texture = manager.petMenu.exploreIcon;

            Debug.Log("Exit playing");

            return manager.idleState;
            //return manager.exploreState;
        }

    }
}
