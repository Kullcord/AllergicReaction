using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenPets : MonoBehaviour
{
    public int maxPets = 5;
    public PetStatsRandomizer randomizer;
    public List<ChosenPetStats> petsChosen = new List<ChosenPetStats>();

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    public void ChoosePet()
    {
        if(petsChosen.Count >= maxPets )
            return;
        ChosenPetStats petStats = new ChosenPetStats();
        petStats.petID = randomizer.petID;
        petStats.allergies = randomizer.allergies;
        petStats.personality = randomizer.personality;
        petStats.curiosity = randomizer.curiosity;
        petStats.attentionSpan = randomizer.attentionSpan;
        petStats.thirst = randomizer.thirst;
        petStats.hunger = randomizer.hunger;
        petStats.love = randomizer.love;
        
        petsChosen.Add(petStats);
    }
}
[Serializable]
public class ChosenPetStats
{
    public int petID;//an id to recognize to which pet these stats are for
    [Space]
    public SymptomReaction[] allergies;
    public float personality;
    public float curiosity;
    public float attentionSpan;
    public float thirst;
    public float hunger;
    public float love;
}
