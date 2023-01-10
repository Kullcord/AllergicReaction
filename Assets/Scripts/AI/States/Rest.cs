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
            manager.animControl.SetBool("Sit", true);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Idle", false);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Eat", false);
            manager.animControl.SetBool("Need", false);
            manager.animControl.SetBool("Allergy", false);

            manager.currentTime += Time.deltaTime;

            manager.petMenu.actionIcon.texture = manager.petMenu.restIcon;

            return this;
        }
        else
        {
            manager.currentTime = 0.0f;

            return manager.exploreState;
        }
    }
}
