using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PetStatsRandomizer : MonoBehaviour
{
    public int petID;//an id to recognize to which pet these stats are for
    [Space]
    public SymptomReaction[] allergies;
    [Range(0, 1)] public float personality;
    [Range(0, 1)] public float curiosity;
    [Range(0, 1)] public float attentionSpan;
    [Range(0, 1)] public float thirst;
    [Range(0, 1)] public float hunger;
    [Range(0, 1)] public float love;

    [Tooltip("Add all item obj here")]
    public Item[] allItems;
    private List<Item> allAllergenItems = new List<Item>();
    private List<Item> allAllergensUsed;
    public SymptomsScriptableObj[] allSymptoms;
    [Tooltip("Add all item obj here")]
    private List<SymptomsScriptableObj> symptomsUsed;
    private void Start()
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            if (allItems[i].isAllergen)
            {
                allAllergenItems.Add(allItems[i]);
            }
        }
    }

    public void SetStats(int petId)
    { 
        StartCoroutine(SetUpStats(petId));
    }

    IEnumerator SetUpStats(int ID)
    {
        petID = ID;

        allAllergensUsed = new(allAllergenItems);
        symptomsUsed = allSymptoms.ToList();
        
        int randomnr;
        randomnr = Random.Range(1, 5);//random amount of allergens
        allergies = new SymptomReaction[randomnr];

        for (int i = 0; i < randomnr; i++)
        {
            allergies[i] = new SymptomReaction();
            //choose random allergen
            allergies[i].allergenItem = allAllergensUsed[Random.Range(0, allAllergensUsed.Count)];
            allAllergensUsed.Remove(allergies[i].allergenItem);
            //choose random reaction
            allergies[i].symptom = symptomsUsed[Random.Range(0, symptomsUsed.Count)];
            symptomsUsed.Remove(allergies[i].symptom);
        }

        personality = Random.Range(0f, 1f);
        curiosity = Random.Range(0f, 1f);
        attentionSpan = Random.Range(0f, 1f);
        thirst = Random.Range(0f, 1f);
        hunger = Random.Range(0f, 1f);
        love = Random.Range(0f, 1f);
        
        yield return null;
    }
}
[Serializable]
public class SymptomReaction
{
    [FormerlySerializedAs("allergen")] [Tooltip("The allergen causing the reaction")]
    public Item allergenItem;
    [Tooltip("What allergic reaction he can have")]
    public SymptomsScriptableObj symptom;
}