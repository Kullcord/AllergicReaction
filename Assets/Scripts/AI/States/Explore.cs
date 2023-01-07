using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explore : State
{
    #region Fields

    public LayerMask groundLayer;

    public Vector3 walkPoint;
    public Vector3 previousWalkpoint;

    public float walkPointRange;

    private bool walkPointSet;
    private bool inAction = false;

    #endregion

    public override State Act(StateManager manager, CharacterStats stats)
    {
        //Start moving
        Move(manager);

        Vector3 distanceToWalk = transform.position - walkPoint;

        //If walkpoint was reached then decide on what to do next
        if (distanceToWalk.magnitude < 0.5f)
        {
            previousWalkpoint = walkPoint;

            walkPointSet = false;
            walkPoint = Vector3.zero;

            inAction = false;

            //return manager.idleState;
            return manager.decisionState;
        }

        return this;
    }

    private void Move(StateManager manager)
    {
        //If I don't have a walk point, then get me one
        if (!walkPointSet)
            SearchWalkPoint(manager);

        //If I have a walk point take me there
        else
            if (!inAction)
            {
                manager.agent.SetDestination(walkPoint);

                inAction = true;
            }
    }

    private void SearchWalkPoint(StateManager manager)
    {
        while (!walkPointSet)
        {
            //Calculate random point in range
            float randomZ = Random.Range(-walkPointRange, walkPointRange);
            float randomX = Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

            //Check if the walk point is reachable && If the AI can walk to the new point
            if (Physics.Raycast(walkPoint, -transform.up, groundLayer) && CanWalkCheck(manager))
                walkPointSet = true;
            else
                walkPointSet = false;
        }
    }

    private bool CanWalkCheck(StateManager manager)
    {
        //Check if the new walkpoint is different than the old one
        //Also check if the pet can go to the walk point (if it is energic enough)
        if (previousWalkpoint != walkPoint)
        {
            if (IsWalkPointDifferentThanPreviousPoint(manager) && CanWalkThatFar(manager))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    //Check if the distance between the new point and the old one is bigger than a number;
    private bool IsWalkPointDifferentThanPreviousPoint(StateManager manager)
    {
        float distBetweenPoints = Vector3.Distance(previousWalkpoint, walkPoint);

        if (distBetweenPoints > 0.25f)
            return true;
        else
            return false;
    }

    //Check if the pet is able to go as far as the walk point;
    private bool CanWalkThatFar(StateManager manager)
    {
        float distAbleToGo = manager.stats.energy * 10f;
        float distanceToGo = Vector3.Distance(manager.transform.position, walkPoint);

        if (distanceToGo <= distAbleToGo)
            return true;
        else
            return false;
    }
}


