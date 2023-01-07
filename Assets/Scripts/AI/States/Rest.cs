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

            manager.currentTime += Time.deltaTime;

            return this;
        }
        else
        {
            manager.currentTime = 0.0f;

            return manager.exploreState;
        }
    }
}
