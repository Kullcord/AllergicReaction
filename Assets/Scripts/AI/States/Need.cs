using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Need : State
{
    [SerializeField] private float rotationSpeed;
    /*[HideInInspector]*/ public bool doOnce;
    public override State Act(StateManager manager, CharacterStats stats)
    {
        manager.agent.isStopped = true;

        if (!stats.allergicReaction)
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
            manager.animControl.SetBool("Pet", false);

            manager.petMenu.actionIcon.texture = manager.petMenu.needsIcon;
            manager.actionIcon.texture = manager.needsIcon;
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
            manager.animControl.SetBool("Pet", false);

            manager.petMenu.actionIcon.texture = manager.petMenu.allergyIcon;
            manager.actionIcon.texture = manager.allergyIcon;

        }

        manager.agent.isStopped = true;
        manager.agent.velocity = Vector3.zero;
        manager.agent.SetDestination(manager.agent.transform.position);

        //if 1on1screenActive then return that state
        /*if (!doOnce)
        {
            doOnce = true;
            manager.agent.isStopped = true;
            manager.agent.velocity = Vector3.zero;
            //manager.agent.updateRotation = false;

            transform.LookAt(manager.mainCamera.transform.up);

        }*/

        return this;
    }
}
