using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class StateManager : MonoBehaviour
{
    #region Fields

    /*[SerializeField] private*/ public int id;

    [Header("Refferences")]
    public CharacterStats stats;
    public NavMeshAgent agent;
    public StatsUIManager petMenu;
    public Animator animControl;

    [Header("Movement")]
    public LayerMask groundLayer;
    public Vector3 walkPoint;
    public Vector3 previousWalkpoint;
    public NavMeshPath path;

    [Header("Item Detection")]
    public LayerMask detectionLayer;
    public GameObject objectToInvestigate;
    public ItemScriptObj containedAllergen;
    public List<GameObject> goList = new List<GameObject>();
    public List<GameObject> previeousObject = new List<GameObject>();

    [Header ("States")]
    public State currentState;
    [HideInInspector] public Explore exploreState;
    [HideInInspector] public Smell smellState;
    [HideInInspector] public Dig digState;
    [HideInInspector] public DecisionMaking decisionState;
    [HideInInspector] public Playing playState;
    [HideInInspector] public Need needState;
    [HideInInspector] public Idle idleState;
    [HideInInspector] public Rest restState;
    [HideInInspector] public Eat eatState;
    [HideInInspector] public Individual individualState;

    [Header("Attention Span")]
    [Range(0.0f, 1000.0f)]
    public float maxTime;
    public float currentTime;

    [HideInInspector] public Bounds bndFloor;
    [HideInInspector] public bool startAllergicReaction = false;
    [HideInInspector] public GameObject individualCamera;
    [HideInInspector] public RawImage actionIcon;

    [Header("Action Textures")]
    public Texture exploreIcon;
    public Texture digIcon;
    public Texture smellIcon;
    public Texture playIcon;
    public Texture needsIcon;
    public Texture restIcon;
    public Texture allergyIcon;

    #endregion

    private void Awake()
    {
        //Initialization stuff
        stats = GetComponent<CharacterStats>();

        id = stats.petID;

        bndFloor = GameObject.Find("MainZone").GetComponent<MeshRenderer>().bounds;
        path = new NavMeshPath();

        exploreState = GetComponent<Explore>();
        smellState = GetComponent<Smell>();
        digState = GetComponent<Dig>();
        decisionState = GetComponent<DecisionMaking>();
        playState = GetComponent<Playing>();
        needState = GetComponent<Need>();
        idleState = GetComponent<Idle>();
        restState = GetComponent<Rest>();
        eatState = GetComponent<Eat>();
        individualState = GetComponent<Individual>();

        currentState = exploreState;
    }

    private void Start()
    {
        petMenu.actionIcon.texture = petMenu.exploreIcon;
    }

    private void Update()
    {
        //Check if the stats of the pet has same ID with the AI, if not give error;
        if (id != stats.petID)
        {
            Debug.LogError("PetID: " + stats.petID + " does not match AI id: " + id);
            return;
        }

        if(previeousObject.Count >= 3)
        {
            previeousObject.RemoveRange(0, previeousObject.Count - 1);
        }

        //Start state machine handle
        HandleStateMachine();
    }

    //Handles states
    private void HandleStateMachine()
    {
        //if state is not null, then perform current state
        if (currentState != null)
        {
            State nextState = currentState.Act(this, stats);

            //If there is a next state to go to, go to it
            if (nextState != null)
                SwitchToNext(nextState);
        }
        else
            SwitchToNext(exploreState);
    }

    //State change
    public void SwitchToNext(State state)
    {
        currentState = state;
    }

    //Eating
    public void Eat(GameObject obj)
    {
        obj.SetActive(false);

        containedAllergen = objectToInvestigate.GetComponent<ItemPhysicalOBJ>().itemObj;

        //If allergy
        if (containedAllergen.isAllergen)
        {
            for(int i = 0; i <= stats.allergies.Length; i++)
            {
                if (stats.allergies[i].allergenItemScriptObj == containedAllergen && !startAllergicReaction)
                {
                    stats.overide = true;
                    startAllergicReaction = true;
                }
            }
        }

        //also decrease the hunger stat
        Debug.Log("Decrease hunger");

    }

}



