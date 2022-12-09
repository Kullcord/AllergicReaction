using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smell : State
{
    [Range (0.0f, 1000.0f)]
    [SerializeField] private float maxTime;
    [SerializeField] private float currentTime;
    private bool done = false;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        Vector3 distance = transform.position - manager.objectToInvestigate.transform.position;

        if (currentTime < manager.stats.atention * maxTime)
        {

            if (distance.magnitude < 2.5f)
            {
                /*Stop agent
                * Play animation
                * Need to implement eating mechanic
                * Need to create chance for eating
                * Idk yet how the chance for eating will be
                */
                currentTime += Time.time / 10;

                if (!done)
                {
                    Debug.Log("Smelling");


                    manager.agent.isStopped = true;

                    //manager.agent.SetDestination(manager.agent.transform.position);

                    //if allergic reaction
                    //then start reaction state

                    done = true;
                }

            }
            else
                MoveTowards(manager);


            return this;
        }
        else
        {
            //If(!allergicReaction)
            //{
            currentTime = 0.0f;

            manager.agent.isStopped = false;

            if (!manager.previeousObject.Contains(manager.objectToInvestigate))
                manager.previeousObject.Add(manager.objectToInvestigate);

            manager.Eat(manager.objectToInvestigate);
            manager.goList.Remove(manager.objectToInvestigate);
            manager.objectToInvestigate = null;

            done = false;

            return manager.exploreState;
            //}
            //else
            //{
            // return allergy reaction state
            //}
        }
    }

    private void MoveTowards(StateManager manager)
    {
        manager.agent.SetDestination(manager.objectToInvestigate.transform.position);

        currentTime = 0.0f;
    }

}

