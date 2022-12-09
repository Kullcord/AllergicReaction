using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dig : State
{
    public override State Act(StateManager manager, CharacterStats stats)
    {
        return this;
    }
}

