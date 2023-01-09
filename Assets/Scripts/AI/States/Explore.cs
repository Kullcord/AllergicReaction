using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Explore : State
{
    #region Fields

    public LayerMask groundLayer;

    public Vector3 walkPoint;
    public Vector3 previousWalkpoint;

    public float walkPointRange;

    private bool walkPointSet;
    private bool inAction = false;

    private bool doOnce = false;

    [SerializeField] private GameObject waypoint;

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
            doOnce = false;

            //return manager.idleState;
            return manager.decisionState;
        }

        if (!doOnce)
        {
            manager.animControl.SetBool("Walk", true);
            manager.animControl.SetBool("Play", false);
            manager.animControl.SetBool("Smell", false);
            manager.animControl.SetBool("Dig", false);
            manager.animControl.SetBool("Idle", false);
            manager.animControl.SetBool("Sit", false);
            manager.animControl.SetBool("Sleep", false);
            manager.animControl.SetBool("Eat", false);
            manager.animControl.SetBool("Need", false);

            doOnce = true;
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
        {
            if (!inAction)
            {
                manager.agent.SetDestination(walkPoint);

                inAction = true;
            }

        }
    }

    #region Movement Position Calculations & Checks

    private void SearchWalkPoint(StateManager manager)
    {
        while (!walkPointSet)
        {
            //Calculate random point in range
            float randomX = Random.Range(manager.bndFloor.min.x, manager.bndFloor.max.x);//(-walkPointRange, walkPointRange);
            float randomZ = Random.Range(manager.bndFloor.min.z, manager.bndFloor.max.z);//(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(randomX, transform.position.y, randomZ);
            waypoint.transform.position = walkPoint;

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
            if (LastPointDistance(manager) && CanWalkThatFar(manager) && IsPointOnPath(manager))
                return true;
            else
                return false;
        }
        else
            return false;
    }

    //Check if the distance between the new point and the old one is bigger than a number;
    private bool LastPointDistance(StateManager manager)
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
        float distAbleToGo = manager.stats.energy * 5f;
        float distanceToGo = Vector3.Distance(manager.transform.position, walkPoint);

        if (distanceToGo <= distAbleToGo && distanceToGo > distAbleToGo / 2)
            return true;
        else
            return false;
    }

    //Checks if the random point is on the nav mesh area
    private bool IsPointOnPath(StateManager manager)
    {
        NavMeshPath path = new NavMeshPath();
        manager.agent.CalculatePath(walkPoint, path);
        if (path.status != NavMeshPathStatus.PathPartial)
            return true;
        else
            return false;
    }

    #endregion
}


