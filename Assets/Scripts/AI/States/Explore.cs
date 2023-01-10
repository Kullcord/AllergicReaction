using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Explore : State
{
    #region Fields

    private bool walkPointSet;
    private bool inAction = false;
    public bool doOnce;

    [SerializeField] private GameObject waypoint;

    #endregion

    public override State Act(StateManager manager, CharacterStats stats)
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
        manager.animControl.SetBool("Allergy", false);

        manager.agent.isStopped = false;

        //Start moving
        if (!doOnce)
        {
            StartCoroutine(Move(manager));
            doOnce = true;
        }
        
        Vector3 distanceToWalk = manager.transform.position - manager.walkPoint;

        //If walkpoint was reached then decide on what to do next
        if (distanceToWalk.magnitude < 1.0f)
        {
            manager.previousWalkpoint = manager.walkPoint;

            walkPointSet = false;
            manager.walkPoint = Vector3.zero;

            inAction = false;
            doOnce = false;
            //return manager.idleState;
            return manager.decisionState;
            
        }


        return this;
    }

    private IEnumerator Move(StateManager manager)
    {
        // Try to calculate a path
        // If it successfully calculates one, set the command to move there and stop the function.
        // else if the path doesn't work, keep trying until it does.
        
        NavMeshPath path = new NavMeshPath();
        WaitForEndOfFrame frame = new();

        Vector3 destination = SearchWalkPoint(manager);
        int iterations = 0;
        while (!manager.agent.CalculatePath(destination, path) && iterations < 1000)
        {
            iterations++;
            destination = SearchWalkPoint(manager);
            yield return frame;
        }

        if (!inAction)
        {
            //Debug.Log("Reachable");
            manager.agent.SetDestination(destination);
            inAction = true;
        }
    }

    #region Movement Position Calculations & Checks

    private Vector3 SearchWalkPoint(StateManager manager)
    {
        if (!walkPointSet)
        {
            //Calculate random point in range
            float randomX = Random.Range(-manager.stats.energy * 10, manager.stats.energy * 10);//(manager.bndFloor.min.x, manager.bndFloor.max.x);
            float randomZ = Random.Range(-manager.stats.energy * 10, manager.stats.energy * 10);//(manager.bndFloor.min.z, manager.bndFloor.max.z);

            manager.walkPoint = new Vector3(randomX, transform.position.y, randomZ);
            waypoint.transform.position = manager.walkPoint;

            //Check if the walk point is reachable && If the AI can walk to the new point
            if ( /*Physics.Raycast(manager.walkPoint, -transform.up, manager.groundLayer) &&*/ CanWalkCheck(manager))
            {
                walkPointSet = true;
                return manager.walkPoint;
            }
        }
        else
        {
            return manager.walkPoint;
        }
        
        return manager.walkPoint;
    }

    private bool CanWalkCheck(StateManager manager)
    {
        //Check if the new walkpoint is different than the old one
        //Also check if the pet can go to the walk point (if it is energic enough)
        if (manager.previousWalkpoint != manager.walkPoint)
        {
            if (LastPointDistance(manager) && CanWalkThatFar(manager))
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
        float distBetweenPoints = Vector3.Distance(manager.previousWalkpoint, manager.walkPoint);

        if (distBetweenPoints > 0.25f)
            return true;
        else
            return false;
    }

    //Check if the pet is able to go as far as the walk point;
    private bool CanWalkThatFar(StateManager manager)
    {
        float distAbleToGo = manager.stats.energy * 10f;
        float distanceToGo = Vector3.Distance(manager.transform.position, manager.walkPoint);

        if (distanceToGo <= distAbleToGo) //&& distanceToGo > distAbleToGo / 2)
            return true;
        else
            return false;
    }

    #endregion
}


