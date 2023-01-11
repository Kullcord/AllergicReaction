using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rest : State
{
    public override State Act(StateManager manager, CharacterStats stats)
    {
        if(manager.currentTime < stats.atention * manager.maxTime)
        {
            //Play resting anim
            manager.animControl.SetBool("Sleep", true);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Idle", false);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Eat", false);
            manager.animControl.SetBool("Need", false);
            manager.animControl.SetBool("Allergy", false);
            manager.animControl.SetBool("Pet", false);

            manager.agent.isStopped = true;
            manager.agent.velocity = Vector3.zero;
            manager.agent.SetDestination(manager.agent.transform.position);

            manager.currentTime += Time.deltaTime;

            return this;
        }
        else
        {
            manager.currentTime = 0.0f;

            /*manager.petMenu.actionIcon.texture = manager.petMenu.exploreIcon;
            manager.actionIcon.texture = manager.exploreIcon;*/

            return manager.decisionState;
        }
    }
}
