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
                //Set icon
                //Start Animation
                manager.animControl.SetBool("Dig", true);
                manager.animControl.SetBool("Play", false);
                manager.animControl.SetBool("Walk", false);
                manager.animControl.SetBool("Smell", false);
                manager.animControl.SetBool("Idle", false);
                manager.animControl.SetBool("Sit", false);
                manager.animControl.SetBool("Sleep", false);
                manager.animControl.SetBool("Eat", false);
                manager.animControl.SetBool("Need", false);
                manager.animControl.SetBool("Allergy", false);
                manager.animControl.SetBool("Pet", false);


                manager.agent.isStopped = true;
                manager.agent.velocity = Vector3.zero;
                manager.agent.SetDestination(manager.agent.transform.position);

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

            manager.petMenu.actionIcon.texture = manager.exploreIcon;
            manager.actionIcon.texture = manager.exploreIcon;

            Debug.Log("Exit digging");

            return manager.idleState;
            //return manager.exploreState;
        }
    }
}

