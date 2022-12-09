using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Allergy : MonoBehaviour
{
    public SymptomReaction[] reaction = new SymptomReaction[1];
    public bool hasAllergicReaction;

    public Slider allergyMeter;
    void Start()
    {
        allergyMeter.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Starts a reaction after an allergen has been eaten
    /// </summary>
    public void AllergenIngested()
    {
        //the allergen meter is full, 
    }
}

public static class Symptoms{
    public enum Reactions
    {
        Itching,
        Wheezing,
        Vomiting,
        Swelling,
        Anaphylaxis
    }
    
}

