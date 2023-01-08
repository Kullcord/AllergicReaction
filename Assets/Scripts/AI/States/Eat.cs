using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eat : State
{
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

            manager.currentTime += Time.deltaTime;

            manager.Eat(manager.objectToInvestigate);

            return this;
        }
        else
        {
            manager.currentTime = 0.0f;

            manager.agent.isStopped = false;

            return manager.exploreState;
        }
    }
}
