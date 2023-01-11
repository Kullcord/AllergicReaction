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

    [HideInInspector]public float hungerLevel;
    [HideInInspector]public float thirstLevel;
    [HideInInspector]public float boredomeLevel;
    [HideInInspector]public float loveLevel;
    
    [HideInInspector]public bool eating;
    [HideInInspector]public bool drinking;
    [HideInInspector]public bool playing;
    [HideInInspector]public bool isLoved;

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
        if (hungerLevel > 25.0f && !eating)
        {
            hungerLevel = hungerLevel - hungerMultiplier * Time.deltaTime;
            stats.hunger = hungerLevel;
            
        }
        else if(hungerLevel <= 25.0f)
            stats.isHungry = true;
    }

    private void ThirstTracker()
    {
        if (thirstLevel > 20.0f && !drinking)
        {
            thirstLevel = thirstLevel - thirstMultiplier * Time.deltaTime;
            stats.thirst = thirstLevel;
            stats.isThirsty = false;
        }
        else if(thirstLevel <= 25.0f)
            stats.isThirsty = true;
    }

    private void BoredomeTracker()
    {
        if (boredomeLevel > 20.0f && !playing)
        {
            boredomeLevel = boredomeLevel - boredomeMultiplier * Time.deltaTime;
            stats.boredome = boredomeLevel;
            stats.isBored = false;
        }
        else if(boredomeLevel <= 25.0f)
            stats.isBored = true;
    }
    
    private void LoveTracker()
    {
        if (loveLevel > 25.0f && !isLoved)
        {
            loveLevel = loveLevel - loveMultiplier * Time.deltaTime;
            stats.love = loveLevel;
            stats.wantsLove = false;
        }
        else if(loveLevel <= 25.0f)
            stats.wantsLove = true;
    }
}
