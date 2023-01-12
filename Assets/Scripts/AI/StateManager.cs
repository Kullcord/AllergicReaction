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
    public Vector3 walkPoint;
    public Vector3 previousWalkpoint;

    [Header("Item Detection")]
    public LayerMask detectionLayer;
    public GameObject objectToInvestigate;
    public ItemSO containedAllergen;
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
    [HideInInspector] public Camera mainCamera;
    [HideInInspector] public GameObject individualCamera;
     public RawImage actionIcon;

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
        mainCamera = Camera.main;
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
            for(int i = 0; i < stats.allergies.Length; i++)
            {
                if (stats.allergies[i].allergenItemSo == containedAllergen && !stats.allergicReaction)
                {
                    stats.overide = true;
                    stats.allergicReaction = true;
                    stats.currentReaction = stats.allergies[i];
                    petMenu.currentAllergy.gameObject.SetActive(true);
                    
                    //show in the stat panel what type of allergy he has

                    #region Setting Allergy Icon

                    switch (stats.currentReaction.symptom)
                    {
                        case Symptoms.Reactions.Itching:
                            switch (stats.allergies[i].allergenItemSo.itemType)
                            {
                                case ItemSO.ItemType.Milk:
                                    petMenu.currentAllergy.sprite = petMenu.Milk[0];
                                    break;
                                case ItemSO.ItemType.Wheat:
                                    petMenu.currentAllergy.sprite = petMenu.Wheat[0];
                                    break;
                                case ItemSO.ItemType.Peanut:
                                    petMenu.currentAllergy.sprite = petMenu.Peanut[0];
                                    break;
                                case ItemSO.ItemType.Cashew:
                                    petMenu.currentAllergy.sprite = petMenu.Cashew[0];
                                    break;
                            }
                            break;
                        case Symptoms.Reactions.Wheezing:
                            switch (stats.allergies[i].allergenItemSo.itemType)
                            {
                                case ItemSO.ItemType.Milk:
                                    petMenu.currentAllergy.sprite = petMenu.Milk[1];
                                    break;
                                case ItemSO.ItemType.Wheat:
                                    petMenu.currentAllergy.sprite = petMenu.Wheat[1];
                                    break;
                                case ItemSO.ItemType.Peanut:
                                    petMenu.currentAllergy.sprite = petMenu.Peanut[1];
                                    break;
                                case ItemSO.ItemType.Cashew:
                                    petMenu.currentAllergy.sprite = petMenu.Cashew[1];
                                    break;
                            }
                            break;
                        case Symptoms.Reactions.Vomiting:
                            switch (stats.allergies[i].allergenItemSo.itemType)
                            {
                                case ItemSO.ItemType.Milk:
                                    petMenu.currentAllergy.sprite = petMenu.Milk[2];
                                    break;
                                case ItemSO.ItemType.Wheat:
                                    petMenu.currentAllergy.sprite = petMenu.Wheat[2];
                                    break;
                                case ItemSO.ItemType.Peanut:
                                    petMenu.currentAllergy.sprite = petMenu.Peanut[2];
                                    break;
                                case ItemSO.ItemType.Cashew:
                                    petMenu.currentAllergy.sprite = petMenu.Cashew[2];
                                    break;
                            }
                            break;
                        case Symptoms.Reactions.Swelling:
                            switch (stats.allergies[i].allergenItemSo.itemType)
                            {
                                case ItemSO.ItemType.Milk:
                                    petMenu.currentAllergy.sprite = petMenu.Milk[3];
                                    break;
                                case ItemSO.ItemType.Wheat:
                                    petMenu.currentAllergy.sprite = petMenu.Wheat[3];
                                    break;
                                case ItemSO.ItemType.Peanut:
                                    petMenu.currentAllergy.sprite = petMenu.Peanut[3];
                                    break;
                                case ItemSO.ItemType.Cashew:
                                    petMenu.currentAllergy.sprite = petMenu.Cashew[3];
                                    break;
                            }
                            break;
                        case Symptoms.Reactions.Anaphylaxis:
                            switch (stats.allergies[i].allergenItemSo.itemType)
                            {
                                case ItemSO.ItemType.Milk:
                                    petMenu.currentAllergy.sprite = petMenu.Milk[4];
                                    break;
                                case ItemSO.ItemType.Wheat:
                                    petMenu.currentAllergy.sprite = petMenu.Wheat[4];
                                    break;
                                case ItemSO.ItemType.Peanut:
                                    petMenu.currentAllergy.sprite = petMenu.Peanut[4];
                                    break;
                                case ItemSO.ItemType.Cashew:
                                    petMenu.currentAllergy.sprite = petMenu.Cashew[4];
                                    break;
                            }
                            break;
                        
                    }
                    #endregion
                    break;
                }
                else if (stats.allergies[i].allergenItemSo != containedAllergen)
                {
                    petMenu.currentAllergy.gameObject.SetActive(false);
                }
            }
        }
        
        //also increase the hunger stat
        Debug.Log("Increase hunger");
        #region IncreaseHunger
        Ch_StatsManager chStats = GetComponent<Ch_StatsManager>();
        if (stats.currentReaction.allergenItemSo.isFood)
        {
            stats.hunger += stats.currentReaction.allergenItemSo.relievesHunger;
            if (stats.hunger > 100)
                stats.hunger = 100;
            chStats.hungerLevel = stats.hunger;
            if (stats.hunger > 25)
            {
                stats.isHungry = false;
                if(!stats.isThirsty && !stats.isBored && !stats.wantsLove)
                    stats.overide = false;
            }
        }else if (stats.currentReaction.allergenItemSo.isDrink)
        {
            stats.thirst += stats.currentReaction.allergenItemSo.relievesThirst;
            if (stats.thirst > 100)
                stats.thirst = 100;
            chStats.thirstLevel = stats.hunger;
            if (stats.thirst > 25)
            {
                stats.isThirsty = false;
                if(!stats.isHungry && !stats.isBored && !stats.wantsLove)
                    stats.overide = false;
            }
        }
                    
        #endregion
    }

}



