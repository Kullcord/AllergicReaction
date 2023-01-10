using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Individual : State
{
    public override State Act(StateManager manager, CharacterStats stats)
    {
        if(!stats.overide || manager.startAllergicReaction)
        {
            manager.animControl.SetBool("Idle", true);
            manager.animControl.SetBool("Eat", false);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Need", false);
            manager.animControl.SetBool("Allergy", false);

            /*manager.walkPoint = Vector3.zero;*/
            //manager.agent.destination = manager.agent.transform.position;
            manager.agent.isStopped = true;
            manager.currentTime = 0.0f;
        }

        //Create multiple checks
        //Check one: if the pet was fed. then play anim and call eat function
        //Check two: if the use rplayed with the pet, then decrease boredome and play playing animation
        //Check three: if an allergy remedy was given, then play animation and deactivate bools
        //Check four: if the pet was pet, then play animation again
        //la sfarsit de fiecare check, trb return manager.restState


        return this;
    }
}
