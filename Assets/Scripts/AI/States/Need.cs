using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need : State
{
    public override State Act(StateManager manager, CharacterStats stats)
    {
        manager.agent.isStopped = true;

        if (!manager.startAllergicReaction)
        {
            manager.animControl.SetBool("Need", true);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Idle", false);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Eat", false);
            manager.animControl.SetBool("Allergy", false);
        }
        else
        {
            manager.animControl.SetBool("Allergy", true);
            manager.animControl.SetBool("Need", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Walk", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Idle", false);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Eat", false);
        }

        //if 1on1screenActive then return that state
        

        return this;
    }
}
