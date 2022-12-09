using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore : State
{
    public Vector3 walkPoint;
    public Vector3 previousWalkpoint;
    [SerializeField] private bool walkPointSet;
    public float walkPointRange;
    public LayerMask groundLayer;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        Vector3 distanceToWalk = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalk.magnitude < 1f)
        {
            previousWalkpoint = walkPoint;

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
        {
            if (DistanceBetweenWalkPoints())
                manager.agent.SetDestination(walkPoint);
            else
                SearchWalkPoint();
        }
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

    //distance between previews walk point and this one needs to be at least bigger than 5f;
    private bool DistanceBetweenWalkPoints()
    {
        if (previousWalkpoint != walkPoint)
        {
            var distance = Vector3.Distance(previousWalkpoint, walkPoint);

            if (distance > 8.0f)
                return true;
            else
                return false;
        }
        else
            return false;
    }
}


