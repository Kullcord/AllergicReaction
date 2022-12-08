using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChosenPets : MonoBehaviour
{
    public int maxPets = 5;
    public PetStatsRandomizer randomizer;

    public void ChoosePet()
    {
        if(MyPets.petsChosen.Count >= maxPets )
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
        
        MyPets.petsChosen.Add(petStats);
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

public static class MyPets
{
    public static List<ChosenPetStats> petsChosen = new List<ChosenPetStats>();
}
