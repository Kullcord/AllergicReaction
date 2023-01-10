using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    #region Fields

    public bool overide = false;
    public bool allergicReaction = false;
    
    [Header("Allergens")]
    public SymptomReaction[] allergies;

    [Header("Pet Stats")]
    public int petID;

    [Range(0.0f, 1.0f)]
    public float personality;

    [Range(0.0f, 1.0f)]
    public float atention;

    [Range(0.5f, 2f)]
    public float energy;
    //might need an energy meter 

    [Range(0.0f, 50.0f)]
    public float curiosity;

    [Header("Hunger")]
    [Range(0.0f, 100.0f)]
    public float hunger;
    public bool isHungry;

    [Header("Thirst")]
    [Range(0.0f, 100.0f)]
    public float thirst;
    public bool isThirsty;

    [Header("Boredome")]
    [Range(0.0f, 100.0f)]
    public float boredome;
    public bool isBored;

    [Header("Love")]
    [Range(0.0f, 1.0f)]
    public float love;

    #endregion
}
