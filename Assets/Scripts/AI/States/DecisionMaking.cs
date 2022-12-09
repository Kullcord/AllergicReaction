using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionMaking : State
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private GameObject objWithSmallestDist;
    private float previousNr;
    private float randomAction;

    private bool doneOnce = false;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        Detection(manager);

        if (!manager.stats.isHungry || !manager.stats.isThirsty)
        {
            if (!doneOnce)
            {
                float rnd = Random.Range(-1.0f, 1.0f);
                randomAction = rnd + manager.stats.curiosity;

                Debug.Log("random action is: " + randomAction);

                if(previousNr != randomAction)
                {
                    if(randomAction > 0)
                    {
                        if (manager.goList.Count != 0)
                        {
                            manager.objectToInvestigate = FindObjWithSmallestDist(manager);
                            //Need to somehow check if there is not another pet near
                            //NEED TO SOMEHOW CHECK IF THE OBJECT TO INVESTIGATE IS ALREADY IN USE

                            if (!manager.previeousObject.Contains(manager.objectToInvestigate) && manager.goList.Contains(manager.objectToInvestigate))
                            {
                                doneOnce = false;

                                for (int i = 0; i < manager.goList.Count; i++)
                                {
                                    manager.goList.RemoveAt(i);
                                }

                                return manager.smellState;
                            }
                        }
                        else
                        {
                            doneOnce = false;

                            return manager.exploreState;
                       
                        }
                    }
                    else if(randomAction <= 0)
                    {
                        doneOnce = false;

                        for (int i = 0; i < manager.goList.Count; i++)
                        {
                            manager.goList.RemoveAt(i);
                        }

                        return manager.digState;
                    }

                    doneOnce = true;
                    previousNr = randomAction;
                }
            } 
        }

        return this;
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

    private void Detection(StateManager manager)
    {
        Collider[] collider = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayer);
        foreach(Collider col in collider)
        {
            if (!manager.goList.Contains(col.gameObject) && !manager.previeousObject.Contains(col.gameObject))
                manager.goList.Add(col.gameObject);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
