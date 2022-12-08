using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class PetsAndStats : MonoBehaviour
{
    [Header("Stats randomizer")]
    public ChosenPetStats randomPet = new ChosenPetStats();
    
    [Tooltip("Add all item obj here")]
    [SerializeField]private Item[] allItems;
    private List<Item> allAllergenItems = new List<Item>();
    private List<Item> allAllergensUsed;
    [Tooltip("Add all item obj here")]
    [SerializeField]private Symptoms.Reactions[] allSymptoms;
    private List<Symptoms.Reactions> symptomsUsed;
    
    [Space][Tooltip("How many pets can you take care of")]
    public int maxPets = 3;
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

    #region Set Random Stats
    public void SetStats(int petId)
    { 
        randomPet.petID = petId;

        allAllergensUsed = new(allAllergenItems);
        symptomsUsed = allSymptoms.ToList();
        
        int randomnr;
        randomnr = Random.Range(1, 5);//random amount of allergens
        randomPet.allergies = new SymptomReaction[randomnr];

        for (int i = 0; i < randomnr; i++)
        {
            randomPet.allergies[i] = new SymptomReaction();
            //choose random allergen
            randomPet.allergies[i].allergenItem = allAllergensUsed[Random.Range(0, allAllergensUsed.Count)];
            allAllergensUsed.Remove(randomPet.allergies[i].allergenItem);
            //choose random reaction
            randomPet.allergies[i].symptom = symptomsUsed[Random.Range(0, symptomsUsed.Count)];
            symptomsUsed.Remove(randomPet.allergies[i].symptom);
        }

        randomPet.personality = Random.Range(0f, 1f);
        randomPet.curiosity = Random.Range(0f, 1f);
        randomPet.attentionSpan = Random.Range(0f, 1f);
        randomPet.thirst = Random.Range(0f, 1f);
        randomPet.hunger = Random.Range(0f, 1f);
        randomPet.love = Random.Range(0f, 1f);
    }
    #endregion

    #region Choose Pets

    public void ChoosePet()
    {
        if(MyPets.petsChosen.Count >= maxPets )
            return;
        ChosenPetStats petStats = new ChosenPetStats();
        petStats.petID = randomPet.petID;
        petStats.allergies = randomPet.allergies;
        petStats.personality = randomPet.personality;
        petStats.curiosity = randomPet.curiosity;
        petStats.attentionSpan = randomPet.attentionSpan;
        petStats.thirst = randomPet.thirst;
        petStats.hunger = randomPet.hunger;
        petStats.love = randomPet.love;
        
        MyPets.petsChosen.Add(petStats);
    }
    #endregion
    
}

/// <summary>
/// A class that contains an allergen item and it's allergic reaction
/// </summary>
[Serializable]
public class SymptomReaction
{
    [Tooltip("The allergen causing the reaction")]
    public Item allergenItem;
    [Tooltip("What allergic reaction he can have")]
    public Symptoms.Reactions symptom;
}

/// <summary>
/// A class that contains a pets traits
/// </summary>
[Serializable]
public class ChosenPetStats
{
    public int petID;//to know what pet image to insert
    [Space]
    public SymptomReaction[] allergies;
    public float personality;
    public float curiosity;
    public float attentionSpan;
    public float thirst;
    public float hunger;
    public float love;
}

/// <summary>
/// A static class that contains a list of all chosen pets
/// </summary>
public static class MyPets
{
    public static List<ChosenPetStats> petsChosen = new List<ChosenPetStats>();
}