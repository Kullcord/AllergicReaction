using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Need : State
{
    public override State Act(StateManager manager, CharacterStats stats)
    {
        if (stats.isHungry)
            Debug.Log("I'm hungry");

        if (stats.isThirsty)
            Debug.Log("I'm thirsty");

        if (stats.isBored)
            Debug.Log("I'm bored");

        return this;
    }
}
