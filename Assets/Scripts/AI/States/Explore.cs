using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Explore : State
{
    #region Fields

    private bool walkPointSet;
    private bool destinationSet = false;
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
        if (distanceToWalk.magnitude < 1.1f)
        {
            manager.previousWalkpoint = manager.walkPoint;

            walkPointSet = false;
            manager.walkPoint = Vector3.zero;

            destinationSet = false;
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
        // else if the path doesn't work, keep trying until iteration is 10
        //If there were no paths found, then retunr idle state (set destination to self)
        
        NavMeshPath path = new NavMeshPath();
        WaitForEndOfFrame frame = new();

        Vector3 destination = SearchWalkPoint(manager);
        int iterations = 0;

        //Check if destination is reachable, if not retry for 9 times
        while (!manager.agent.CalculatePath(destination, path) & iterations < 10)
        {
            iterations++;
            Debug.Log("iterations: " + iterations);
            destination = SearchWalkPoint(manager);
            yield return frame;
        }

        //If there was no valid point, then set the destination to pets position
        if(iterations >= 10)
        {
            Debug.Log("cant find path");
            destination = manager.agent.transform.position;
            manager.walkPoint = destination;
        }

        //Set destination if the destination wasn't set already
        if (!destinationSet)
        {
            manager.agent.SetDestination(destination);
            waypoint.transform.position = destination;
            destinationSet = true;
        }
    }

    #region Movement Position Calculations & Checks

    private Vector3 SearchWalkPoint(StateManager manager)
    {
        //Generate a point inside the pet's range
        //Check if the point is different then the last one, and if the point is reachable
        if (!walkPointSet)
        {
            //Calculate random point in range
            float randomX = Random.Range(-manager.stats.energy * 10, manager.stats.energy * 10);//(manager.bndFloor.min.x, manager.bndFloor.max.x);
            float randomZ = Random.Range(-manager.stats.energy * 10, manager.stats.energy * 10);//(manager.bndFloor.min.z, manager.bndFloor.max.z);

            manager.walkPoint = new Vector3(randomX, transform.position.y, randomZ);

            //Check if the walk point is reachable && If the AI can walk to the new point
            /*Physics.Raycast(manager.walkPoint, -transform.up, manager.groundLayer) &&*/
            if (CanWalkCheck(manager))
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


