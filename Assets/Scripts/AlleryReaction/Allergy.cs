using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Allergy : MonoBehaviour
{
    public SymptomReaction[] reaction = new SymptomReaction[1];
    public bool hasAllergicReaction;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AllergenIngested()
    {
        //if the item eaten is an allergen start reaction 
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

