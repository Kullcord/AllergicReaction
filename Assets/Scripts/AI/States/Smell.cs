using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smell : State
{
    private bool done = false;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        Vector3 distance = transform.position - manager.objectToInvestigate.transform.position;

        //Timer based on the atention span
        if (manager.currentTime < manager.stats.atention * manager.maxTime)
        {
            //If the object the AI wants to investigate is still in the scene then the system can continue with the smell state
            if (manager.objectToInvestigate.activeSelf)
            {
                //If the AI is close enough to investigate, start the smelling process
                if (distance.magnitude < 2.5f)
                {
                    /* Play animation 
                     * Add Icon
                     * Play sound
                     */
                    manager.animControl.SetBool("Smell", true);
                    manager.animControl.SetBool("Play", false);
                    manager.animControl.SetBool("Walk", false);
                    manager.animControl.SetBool("Dig", false);
                    manager.animControl.SetBool("Idle", false);
                    manager.animControl.SetBool("Sit", false);
                    manager.animControl.SetBool("Sleep", false);
                    manager.animControl.SetBool("Eat", false);
                    manager.animControl.SetBool("Need", false);
                    manager.animControl.SetBool("Allergy", false);

                    manager.currentTime += Time.deltaTime;

                    if (!done)
                    {
                        Debug.Log("Smelling");

                        manager.agent.isStopped = true;
                        manager.agent.velocity = Vector3.zero;
                        
                        done = true;
                    }

                }

                //If the ai is not close enough to perform the smelling action, then move towards the object
                else
                {
                    manager.agent.isStopped = false;

                    done = false;

                    MoveTowards(manager);
                }

                return this;
            }
            else
            {
                ResetValues(manager);
                manager.objectToInvestigate = null;


                manager.petMenu.actionIcon.texture = manager.petMenu.exploreIcon;

                Debug.Log("Cant continue smelling");

                return manager.idleState;
                //return manager.exploreState;
            }
        }

        //If the timer runs out, or the object to investigate is no longer in the scene
        else
        {
            if ((manager.stats.hunger < 50 || EatingProbability(manager.stats.curiosity)) && distance.magnitude < 2.5f)
            {
                ResetValues(manager);

                //manager.petMenu.actionIcon.texture = manager.petMenu.exploreIcon;

                Debug.Log("Eating");

                return manager.eatState;
            }
            else
            {
                ResetValues(manager);

                manager.objectToInvestigate = null;

                manager.petMenu.actionIcon.texture = manager.petMenu.exploreIcon;

                Debug.Log("Exit smelling");

                return manager.idleState;
            }
        }
    }

    private bool EatingProbability(float percentage)
    {
        float rnd = Random.Range(0, 91);

        if (rnd <= percentage)
            return true;
        else
            return false;
    }

    private void ResetValues(StateManager manager)
    {
        manager.currentTime = 0.0f;

        //manager.agent.isStopped = false;

        if (!manager.previeousObject.Contains(manager.objectToInvestigate))
            manager.previeousObject.Add(manager.objectToInvestigate);

        //manager.objectToInvestigate = null;
        manager.containedAllergen = null;

        done = false;
    }

    private void MoveTowards(StateManager manager)
    {
        manager.agent.SetDestination(manager.objectToInvestigate.transform.position);

        manager.animControl.SetBool("Walk", true);
        manager.animControl.SetBool("Play", false);
        manager.animControl.SetBool("Smell", false);
        manager.animControl.SetBool("Dig", false);
        manager.animControl.SetBool("Idle", false);
        manager.animControl.SetBool("Sit", false);
        manager.animControl.SetBool("Sleep", false);
        manager.animControl.SetBool("Eat", false);
        manager.animControl.SetBool("Need", false);
        manager.animControl.SetBool("Allergy", false);

        manager.currentTime = 0.0f;
    }
}

