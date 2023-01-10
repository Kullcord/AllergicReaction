using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : State
{
    private bool callOnce;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        if (manager.currentTime < (stats.atention * manager.maxTime) / 2)
        {
            //Play resting anim
            manager.animControl.SetBool("Eat", true);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Idle", false);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Need", false);
            manager.animControl.SetBool("Allergy", false);

            manager.currentTime += Time.deltaTime;

            if (!callOnce)
            {
                manager.Eat(manager.objectToInvestigate);
                callOnce = true;
            }

            return this;
        }
        else
        {
            manager.currentTime = 0.0f;

            if (stats.overide)
                return manager.needState;
            else
            {
                manager.agent.isStopped = false;

                manager.petMenu.actionIcon.texture = manager.petMenu.exploreIcon;
                manager.actionIcon.texture = manager.exploreIcon;

                return manager.exploreState;
            }

        }
    }
}
