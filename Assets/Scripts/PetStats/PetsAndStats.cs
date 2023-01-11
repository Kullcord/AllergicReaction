using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PetsAndStats : MonoBehaviour
{
    [Header("Stats randomizer")]
    public ChosenPetStats randomPet = new ChosenPetStats();
    
    [Tooltip("Add all item obj here")]
    [SerializeField]private ItemScriptObj[] allItems;
    private List<ItemScriptObj> allAllergenItems = new List<ItemScriptObj>();
    private List<ItemScriptObj> allAllergensUsed;
    [Tooltip("Add all item obj here")]
    [SerializeField]private Symptoms.Reactions[] allSymptoms;
    
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
    public void SetStats(int petType)
    { 
        randomPet.petType = petType;

        allAllergensUsed = new(allAllergenItems);
        
        int randomnr;
        randomnr = Random.Range(1, 5);//random amount of allergens
        randomPet.allergies = new SymptomReaction[randomnr];

        for (int i = 0; i < randomnr; i++)
        {
            randomPet.allergies[i] = new SymptomReaction();
            //choose random allergen
            randomPet.allergies[i].allergenItemScriptObj = allAllergensUsed[Random.Range(0, allAllergensUsed.Count)];
            allAllergensUsed.Remove(randomPet.allergies[i].allergenItemScriptObj);
            //choose random reaction
            randomPet.allergies[i].symptom = allSymptoms[Random.Range(0, allSymptoms.Length)];
        }

        randomPet.petID = MyPets.petsChosen.Count;
        randomPet.energy = Random.Range(0.5f, 2.1f);
        randomPet.attentionSpan = Random.Range(0.5f, 1.6f);
        randomPet.curiosity = Random.Range(0f, 50.1f);
        randomPet.thirst = Random.Range(0f, 1.1f);
        randomPet.hunger = Random.Range(0f, 1.1f);
        randomPet.boredom = Random.Range(0f, 1.1f);
        randomPet.love = Random.Range(0f, 1.1f);
    }
    #endregion

    #region Choose Pets

    public void ChoosePet()
    {
        if(MyPets.petsChosen.Count >= maxPets )
            return;
        ChosenPetStats petStats = new ChosenPetStats();
        petStats.petID = randomPet.petID;
        petStats.petType = randomPet.petType;
        petStats.allergies = randomPet.allergies;
        petStats.curiosity = randomPet.curiosity;
        petStats.energy = randomPet.energy;
        petStats.attentionSpan = randomPet.attentionSpan;
        petStats.thirst = randomPet.thirst;
        petStats.hunger = randomPet.hunger;
        petStats.boredom = randomPet.boredom;
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
    [FormerlySerializedAs("allergenItem")] [Tooltip("The allergen causing the reaction")]
    public ItemScriptObj allergenItemScriptObj;
    [Tooltip("What allergic reaction he can have")]
    public Symptoms.Reactions symptom;
}

/// <summary>
/// A class that contains a pets traits
/// </summary>
[Serializable]
public class ChosenPetStats
{
    public int petType;//to know what pet image to insert
    public int petID;
    [Space]
    public SymptomReaction[] allergies;
    public float energy;
    public float attentionSpan;
    public float curiosity;
    public float thirst;
    public float hunger;
    public float boredom;
    public float love;
}

/// <summary>
/// A static class that contains a list of all chosen pets
/// </summary>
public static class MyPets
{
    public static List<ChosenPetStats> petsChosen = new List<ChosenPetStats>();
}