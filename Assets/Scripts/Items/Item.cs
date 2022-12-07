using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Items/ItemType")]

public class Item : ScriptableObject
{
    public enum Ingredient
    {
        None,
        Nuts
    }

    public enum ItemType
    {
        Water,
        Ice,
        Cream,
        Pill,
        Inhaler,
        EpiPen,
        Ball,
        Meat,
        Snack
    }

    public Sprite itemSprite;
    public ItemType itemType;
    public List<Ingredient> ingredients;
    
    [Header("Cure Symptoms")]
    public bool isRemedy;//if ticked yes then show next
    public List<RemedyType> remedyTypesAndTheirSuccessRate;//how much is it gonna help with each symptom
}

[Serializable]
public class RemedyType 
{
    public Symptoms.Reaction ReactionType;
    public float successRate;
}
