using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State
{
    private bool doneOnce = false;

    private State previousState;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        //If the probability check is passed, then return rest state,
        //else continue idle state
        if (ProbabilityCheck(stats) && previousState != manager.restState)
        {
            //return rest state
            manager.currentTime = 0.0f;

            doneOnce = false;

            manager.petMenu.actionIcon.texture = manager.petMenu.restIcon;
            manager.actionIcon.texture = manager.restIcon;

            previousState = manager.restState;
            return manager.restState;
        }

        if (manager.currentTime < manager.stats.atention * manager.maxTime)
        {
            //Play idle animation
            manager.animControl.SetBool("Idle", true);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Eat", false);
            manager.animControl.SetBool("Need", false);
            manager.animControl.SetBool("Allergy", false);
            manager.animControl.SetBool("Pet", false);


            manager.currentTime += Time.deltaTime;
        } 
        else
        {
            manager.currentTime = 0.0f;

            doneOnce = false;

            manager.agent.isStopped = false;

            manager.petMenu.actionIcon.texture = manager.petMenu.exploreIcon;
            manager.actionIcon.texture = manager.exploreIcon;

            previousState = manager.idleState;
            return manager.exploreState;
        }

        return this;
    }

    private bool ProbabilityCheck(CharacterStats stats)
    {
        if (!doneOnce)
        {
            float rnd = Random.Range(0, 31);

            float percentage = stats.energy * 10f;

            //Debug.Log("rnd is: " + rnd);
            //Debug.Log("percentage: " + percentage);

            if (percentage <= rnd)
                return true;

            doneOnce = true;
        }
        
        return false;
    }
}
