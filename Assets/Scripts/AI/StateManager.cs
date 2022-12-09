using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    #region Fields

    [Header("Refferences")]
    public CharacterStats stats;
    public State currentState;
    public NavMeshAgent agent;

    [Header("Item Detection")]
    public GameObject objectToInvestigate;
    public List<GameObject> goList = new List<GameObject>();
    public List<GameObject> previeousObject = new List<GameObject>();

    [SerializeField] private bool collidedWithItem = false;
    private bool alreadyDone = false;

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

    public void Eat(GameObject obj)
    {
        obj.SetActive(false);

        //also decrease the hunger stat
        Debug.Log("Decrease hunger");

        //If allergy
        Debug.Log("Do allergy reaction");
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Might need to make a better collision check
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Item"))
            collidedWithItem = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Item"))
            collidedWithItem = false;

        //alreadyDone = false;
    }

    /*public void Drink(GameObject obj)
    {

    }*/
}



