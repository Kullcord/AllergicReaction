using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionMaking : State
{
    #region Fields

    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask detectionLayer;
    private GameObject objWithSmallestDist;

    private int previousNr = -int.MaxValue;
    private int randomAction;

    private bool SmellState = false;
    private bool DigState = false;
    private bool PlayState = false;
    private bool ExploreState = false;

    private bool doneOnce = false;

    #endregion

    public override State Act(StateManager manager, CharacterStats stats)
    {
        Detection(manager);//I think I need to add this in !doneOnce if statement

        //make a bool ovveride that is true whenever the pet is hungry, thirsty, bored or wants love
        if (!manager.stats.overide)
        {
            if (!doneOnce)
            {
                
                StateProbabilityCheck(manager);

                if(SmellState)
                {
                    //This might be better added to smell state
                    //If there are potential items around the pet
                    if (manager.goList.Count != 0)
                    {
                        //Get closest obj from list
                        manager.objectToInvestigate = FindObjWithSmallestDist(manager);

                        //If the object is not already part of the previous and current obj list, then the pet can proceed to smell the item
                        if (!manager.previeousObject.Contains(manager.objectToInvestigate) && manager.goList.Contains(manager.objectToInvestigate))
                        {
                            for (int i = 0; i < manager.goList.Count; i++)
                            {
                                manager.goList.RemoveAt(i);
                            }

                            ResetValues();

                            return manager.smellState;

                        } 
                        else
                        {
                            ResetValues();

                            return manager.exploreState;
                        }
                    }
                    else
                    {
                        ResetValues();

                        return manager.exploreState;
                    }
                }

                if(DigState)
                {
                    for (int i = 0; i < manager.goList.Count; i++)
                    {
                        manager.goList.RemoveAt(i);
                    }

                    ResetValues();

                    return manager.digState;
                }

                if (PlayState)
                {
                    for (int i = 0; i < manager.goList.Count; i++)
                    {
                        manager.goList.RemoveAt(i);
                    }

                    ResetValues();

                    return manager.playState;
                }

                if (ExploreState)
                {
                    for (int i = 0; i < manager.goList.Count; i++)
                    {
                        manager.goList.RemoveAt(i);
                    }

                    ResetValues();

                    return manager.exploreState;
                }

                doneOnce = true;
            }

            return this;
        } 
        else
        {
            for (int i = 0; i < manager.goList.Count; i++)
            {
                manager.goList.RemoveAt(i);
            }

            ResetValues();

            return manager.needState;
        }

        
    }

    /*Probability Check, based on curiosity and energy, transform to percentage and 
    * if percentage higher then a random number, than the pet is going to make one of 
    * following actions: play, dig, smell
    */
    private void StateProbabilityCheck(StateManager manager)
    {
        int rnd = Random.Range(0, 101);

        float percentage = manager.stats.curiosity * manager.stats.energy;

        //Debug.Log("percentage is: " + percentage);

        //Debug.Log("probability is: " + rnd);

        if (rnd <= percentage)
        {
            DecideAction(manager);
            //Might need to change it a bit ->
            // -> If the pet has high curiosity and passes the check, then decide ONLY between DIG and SMELL
        }
        else
            ExploreState = true;
    }

    private void DecideAction(StateManager manager)
    {
       
        int rnd = Random.Range(-1, 2);//need between -1 and 2 because when using random range with ints, the second parameter is seen as max, and max is always exclusive
        randomAction = rnd;

        //Debug.Log("random action is: " + randomAction);

        if (previousNr != randomAction)
        {
            //Might be better with switch
            if (randomAction > 0)
            {
                SmellState = true;
                DigState = false;
                PlayState = false;
                ExploreState = false;

                previousNr = randomAction;
            } 
            else if(randomAction < 0)
            {
                DigState = true;
                SmellState = false;
                PlayState = false;
                ExploreState = false;

                previousNr = randomAction;
            }
            else if (randomAction == 0)
            {
                PlayState = true;
                DigState = false;
                SmellState = false;
                ExploreState = false;

                previousNr = randomAction;
            }
        }
        else
        {
            DecideAction(manager);
        }
    }

    //Will need to add visibility obstructions(eg trees)
    private void Detection(StateManager manager)
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        foreach(Collider col in collider)
        {
            if (!manager.goList.Contains(col.gameObject) && !manager.previeousObject.Contains(col.gameObject))
                manager.goList.Add(col.gameObject);
        }
        
    }

    private GameObject FindObjWithSmallestDist(StateManager manager)
    {
        GameObject nearestObj = null;

        foreach (GameObject obj in manager.goList)
        {
            var nearestDist = float.MaxValue;

            if (Vector3.Distance(obj.transform.position, transform.position) < nearestDist)
            {
                nearestDist = Vector3.Distance(obj.transform.position, transform.position);
                nearestObj = obj;
            }
        }

        return nearestObj;
    }

    private void ResetValues()
    {
        doneOnce = false;

        SmellState = false;
        DigState = false;
        PlayState = false;
        ExploreState = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
