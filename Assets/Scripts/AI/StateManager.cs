using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    #region Fields

    public CharacterStats stats;
    public State currentState;
    public NavMeshAgent agent;
    public List<GameObject> goList = new List<GameObject>();
    public GameObject objectToInvestigate;
    public List<GameObject> previeousObject = new List<GameObject>();

    [Header ("States")]
    public Explore exploreState;
    public Smell smellState;
    public Dig digState;
    public DecisionMaking decisionState;

    //[SerializeField] private List<State> stateList;

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
}



