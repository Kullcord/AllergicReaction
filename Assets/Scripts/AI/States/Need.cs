using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need : State
{
    public override State Act(StateManager manager, CharacterStats stats)
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

        if (stats.isHungry)
            Debug.Log("I'm hungry");

        if (stats.isThirsty)
            Debug.Log("I'm thirsty");

        if (stats.isBored)
            Debug.Log("I'm bored");

        return this;
    }
}
