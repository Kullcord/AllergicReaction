using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smell : State
{
    private bool done = false;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        Vector3 distance = transform.position - manager.objectToInvestigate.transform.position;

        //if(manager.objectToInvestigate != null)
        //{
            if (manager.currentTime < manager.stats.atention * manager.maxTime)
            {
                if (distance.magnitude < 2.5f)
                {
                    /* Play animation
                    * Need to create chance for eating
                    * Idk yet how the chance for eating will be
                    * NEED TO SOMEHOW CHECK IF THE OBJECT TO INVESTIGATE IS ALREADY IN USE
                    */
                    manager.currentTime += Time.deltaTime;

                    if (!done)
                    {
                        Debug.Log("Smelling");

                        manager.agent.isStopped = true;

                        done = true;
                    }

                }
                else
                    MoveTowards(manager);

                return this;
            }
            else
            {
                manager.currentTime = 0.0f;

                manager.agent.isStopped = false;

                if (!manager.previeousObject.Contains(manager.objectToInvestigate))
                    manager.previeousObject.Add(manager.objectToInvestigate);

                manager.Eat(manager.objectToInvestigate);
                /*manager.goList.Remove(manager.objectToInvestigate);*/
                manager.objectToInvestigate = null;
                manager.allergenOBJ = null;

                done = false;

                //If(!allergicReaction)
                //{
                return manager.exploreState;
                //}
                //else
                //{
                // return allergy reaction state
                //}
            }
        //} else
            //return manager.exploreState;
    }

    private void MoveTowards(StateManager manager)
    {
        manager.agent.SetDestination(manager.objectToInvestigate.transform.position);

        manager.currentTime = 0.0f;
    }

}

