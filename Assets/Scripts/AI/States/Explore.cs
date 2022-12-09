using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore : State
{
    public Vector3 walkPoint;
    [SerializeField] private bool walkPointSet;
    public float walkPointRange;
    public LayerMask groundLayer;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        Vector3 distanceToWalk = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalk.magnitude < 1f)
        {
            walkPointSet = false;
            walkPoint = Vector3.zero;

            return manager.decisionState;
        }
        else
        {
            Move(manager);
            
            return this;
        }
    }

    private void Move(StateManager manager)
    {
        if (!walkPointSet)
            SearchWalkPoint();
        else
            manager.agent.SetDestination(walkPoint);
    }

    private void Delayed()
    {
        walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, groundLayer))
            walkPointSet = true;
        else
            walkPointSet = false;
    }
}


