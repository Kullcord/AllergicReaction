using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch_StatsManager : MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private CharacterStats stats;

    [Header("Multipliers")]
    [SerializeField] private float hungerMultiplier;
    [SerializeField] private float thirstMultiplier;
    [SerializeField] private float boredomeMultiplier;

    private float hungerLevel;
    private float thirstLevel;
    private float boredomeLevel;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();

        id = stats.petID;

        hungerLevel = stats.hunger;
        thirstLevel = stats.thirst;
        boredomeLevel = stats.boredome;

        hungerMultiplier = hungerMultiplier * stats.energy;
        thirstMultiplier = thirstMultiplier * stats.energy;
        boredomeMultiplier = boredomeMultiplier * stats.energy;
    }

    private void Update()
    {
        //Check if stats ID is the same as this ID
        if (id != stats.petID)
        {
            Debug.LogError("PetID: " + stats.petID + " does not match AI id: " + id);
            return;
        }

        StatsManager();
    }

    private void StatsManager()
    {
        if (!stats.isHungry && !stats.isThirsty && !stats.isBored)
        {
            HungerTracker();

            ThirstTracker();

            BoredomeTracker();
        }
        else
            stats.overide = true;
    }

    private void HungerTracker()
    {
        if (hungerLevel > 25.0f)
        {
            hungerLevel = hungerLevel - hungerMultiplier * stats.energy * Time.deltaTime;
            stats.hunger = hungerLevel;
        }
        else
            stats.isHungry = true;
    }

    private void ThirstTracker()
    {
        if (thirstLevel > 20.0f)
        {
            thirstLevel = thirstLevel - thirstMultiplier * Time.deltaTime;
            stats.thirst = thirstLevel;
        }
        else
            stats.isThirsty = true;
    }

    private void BoredomeTracker()
    {
        if (boredomeLevel > 20.0f)
        {
            boredomeLevel = boredomeLevel - boredomeMultiplier * Time.deltaTime;
            stats.boredome = boredomeLevel;
        }
        else
            stats.isBored = true;
    }
}
