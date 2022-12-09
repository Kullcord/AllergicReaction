using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecisionMaking : State
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private LayerMask detectionLayer;
    private bool doneOnce = false;

    public override State Act(StateManager manager, CharacterStats stats)
    {
        Detection(manager);


        if (!manager.stats.isHungry || !manager.stats.isThirsty)
        {
            /*Here I will need to create a chance calculator basically
            * It will calculate based on the curiosity stat,
            * My idea at the moment is:
            * Create a random number from -1 to 1, 
            * then add the curiosity level
            * and then if the number is positive( > 0) then smell
            * if the number is negative(<0) then explore
            * I need to also think of a way of choosing between digging or smelling
            */
            if (manager.goList.Count != 0)
            {
                /*For the choice between dig or smell, 
                 * One is true one is false, for example:
                 * Dig is true
                 * Smell is false
                 * Create a bool that would randomly generate one of this two
                 * Then if true execute dig
                 * if false execute smell
                 */
                if (manager.stats.curiosity > 0.5f && !doneOnce)
                {
                    doneOnce = true;

                    int randomOBJ = Random.Range(0, manager.goList.Count);

                    manager.objectToInvestigate = manager.goList[randomOBJ];
                    //manager.goList.Remove(manager.objectToInvestigate);

                    if (!manager.previeousObject.Contains(manager.objectToInvestigate) &&
                        manager.goList.Contains(manager.objectToInvestigate))
                    {
                        doneOnce = false;

                        return manager.smellState;
                    }
                }
                else
                    return manager.exploreState;
            }
            else
                return manager.exploreState;
        }

        return this;
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
