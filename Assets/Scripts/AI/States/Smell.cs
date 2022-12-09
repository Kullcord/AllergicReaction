using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smell : State
{
    [Range (0.0f, 1000.0f)]
    [SerializeField] private float maxTime;
    [SerializeField] private float currentTime;

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
                Debug.Log("Smelling");

                currentTime += Time.time / 10;

                manager.agent.isStopped = true;
                //manager.agent.SetDestination(manager.agent.transform.position);
            }
            else
                MoveTowards(manager);

            return this;
        }
        else
        {
            currentTime = 0.0f;

            manager.agent.isStopped = false;

            if (!manager.previeousObject.Contains(manager.objectToInvestigate))
                manager.previeousObject.Add(manager.objectToInvestigate);

            manager.objectToInvestigate = null;

            return manager.exploreState;

        }
    }

    private void MoveTowards(StateManager manager)
    {
        manager.agent.SetDestination(manager.objectToInvestigate.transform.position);
    }

}

