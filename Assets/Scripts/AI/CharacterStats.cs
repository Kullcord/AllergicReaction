using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    #region Fields

    [Header("Allergens")]
    public List<Item> allergends;
    public List<Symptoms.Reactions> allergyLevel;

    [Header("Pet Stats")]
    public int petID;

    [Range(0.0f, 1.0f)]
    public float personality;

    [Range(0.0f, 1.0f)]
    public float atention;

    [Range(0.0f, 1.0f)]
    public float curiosity;

    [Range(0.0f, 1.0f)]
    public float thirst;
    private float thirstLevel;
    public bool isThirsty;

    [Range(0.0f, 1.0f)]
    public float hunger;
    private float hungerLevel;
    public bool isHungry;

    [Range(0.0f, 1.0f)]
    public float love;

    #endregion

    private void Start()
    {
        //here initialize the stats from the selection menu
    }
}
