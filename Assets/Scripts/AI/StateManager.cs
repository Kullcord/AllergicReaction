using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private int id;

    [Header("Refferences")]
    public CharacterStats stats;
    public NavMeshAgent agent;

    [Header("Item Detection")]
    public GameObject objectToInvestigate;
    public List<GameObject> goList = new List<GameObject>();
    public List<GameObject> previeousObject = new List<GameObject>();
    public ItemScriptObj allergenOBJ;

    [Header ("States")]
    public State currentState;
    public Explore exploreState;
    public Smell smellState;
    public Dig digState;
    public DecisionMaking decisionState;

    [Header("Attention Span")]
    [Range(0.0f, 1000.0f)]
    public float maxTime;
    public float currentTime;

    #endregion

    private void Awake()
    {

        stats = GetComponent<CharacterStats>();

        exploreState = GetComponent<Explore>();
        smellState = GetComponent<Smell>();
        digState = GetComponent<Dig>();
        decisionState = GetComponent<DecisionMaking>();
    }

    private void Update()
    {
        if (id != stats.petID)
        {
            Debug.LogError("PetID: " + stats.petID + " does not match AI id: " + id);
            return;
        }

        HandleStateMachine();
    }

    private void HandleStateMachine()
    {
        if(currentState != null)
        {
            State nextState = currentState.Act(this, stats);

            if (nextState != null)
                SwitchToNext(nextState);
        }
    }

    private void SwitchToNext(State state)
    {
        currentState = state;
    }

    public void Eat(GameObject obj)
    {
        obj.SetActive(false);

        allergenOBJ = objectToInvestigate.GetComponent<ItemPhysicalOBJ>().itemObj;

        //If allergy
        if (allergenOBJ.isAllergen)
        {

            if (stats.allergends.Contains(allergenOBJ))
                Debug.Log("Do Allergic reaction");

        }

        //also decrease the hunger stat
        Debug.Log("Decrease hunger");

    }

}



