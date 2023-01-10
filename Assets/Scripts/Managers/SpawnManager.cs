using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> petsObj;
    [SerializeField] private List<GameObject> statsBars;

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    { 
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < MyPets.petsChosen.Count; i++)
        {
            petsObj[i].SetActive(true);
            statsBars[i].SetActive(true);

            ChosenPetStats currentPet = MyPets.petsChosen[i];
            CharacterStats petStats = petsObj[i].GetComponent<CharacterStats>();
            Ch_StatsManager petStatsManager = petsObj[i].GetComponent<Ch_StatsManager>();

            petStats.petID = currentPet.petID;
            petStats.atention = currentPet.attentionSpan;
            petStats.energy = currentPet.energy;
            petStats.curiosity = currentPet.curiosity;
            petStats.allergies = currentPet.allergies;

            petStatsManager.id = currentPet.petID;
            petStatsManager.hungerMultiplier = currentPet.hunger;
            petStatsManager.thirstMultiplier = currentPet.thirst;
            petStatsManager.boredomeMultiplier = currentPet.boredom;
            petStatsManager.loveMultiplier = currentPet.love;
        }
    }
}
