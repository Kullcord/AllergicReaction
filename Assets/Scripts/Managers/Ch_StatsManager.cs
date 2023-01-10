using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ch_StatsManager : MonoBehaviour
{
    public int id;
    [SerializeField] private CharacterStats stats;

    [Header("Multipliers")]
    public float hungerMultiplier;
    public float thirstMultiplier;
    public float boredomeMultiplier;
    public float loveMultiplier;

    private float hungerLevel;
    private float thirstLevel;
    private float boredomeLevel;
    private float loveLevel;

    private void Start()
    {
        stats = GetComponent<CharacterStats>();

        id = stats.petID;

        hungerLevel = stats.hunger;
        thirstLevel = stats.thirst;
        boredomeLevel = stats.boredome;
        loveLevel = stats.love;

        hungerMultiplier *= stats.energy;
        thirstMultiplier *= stats.energy;
        boredomeMultiplier *= stats.energy;
        loveMultiplier *= stats.energy;
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
        if (!stats.isHungry)
            HungerTracker();
        if(!stats.isThirsty)
            ThirstTracker();
        if(!stats.isBored)
            BoredomeTracker();
        if(!stats.wantsLove)
            LoveTracker();
            
        
        if(stats.isHungry || stats.isBored || stats.isThirsty || stats.wantsLove || stats.allergicReaction)
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
    
    private void LoveTracker()
    {
        if (loveLevel > 25.0f)
        {
            loveLevel = loveLevel - loveMultiplier * Time.deltaTime;
            stats.love = loveLevel;
        }
        else
            stats.wantsLove = true;
    }
}
