using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PetStatsText : MonoBehaviour
{
    [SerializeField]private PetStatsRandomizer statsRandomizer;
    public TextMeshProUGUI[] petInfoText;
    //For Showing Chosen Pets
    public ChosenPets chosenPetsScript;
    public TextMeshProUGUI[] petsChosenText;

    public int currentSelectedPet;
    private void Start()
    {
        for (int i = 0; i < petsChosenText.Length; i++)
        {
            petsChosenText[i].text = "No pet chosen";
        }
    }

    public void UpdateChosenPets()
    {
        for (int i = 0; i < chosenPetsScript.petsChosen.Count; i++)
        {
            petsChosenText[i].text = chosenPetsScript.petsChosen[i].petID + " chosen";
        }
    }
    
    public void DeletePet()
    {
        if (chosenPetsScript.petsChosen.Count == 0 || chosenPetsScript.petsChosen[currentSelectedPet] == null)
        {
            print("there is no pet to remove");
            return;
        }
        if (chosenPetsScript.petsChosen[currentSelectedPet] != null)
        {
            chosenPetsScript.petsChosen.Remove(chosenPetsScript.petsChosen[currentSelectedPet]);
            petsChosenText[currentSelectedPet].text = "No pet chosen";
            UpdateChosenPets();
        }
    }

    public void SetUpTextInfo(int whichPet = 10)
    {
        if (whichPet == 10) //show info about a randomized pet
        {
            petInfoText[0].text = "Pet Id: " + statsRandomizer.petID;
            string allergensText = "";
            string symptomsText = "";
            for (int index = 0; index < statsRandomizer.allergies.Length; index++)
            {
                allergensText += " - " + statsRandomizer.allergies[index].allergenItem.itemType;
                symptomsText += " - " + statsRandomizer.allergies[index].symptom.symptomType;
            }

            petInfoText[1].text = "Allergens: " + allergensText;
            petInfoText[2].text = "Symptoms: " + symptomsText;
            petInfoText[3].text = "Personality: " + statsRandomizer.personality;
            petInfoText[4].text = "Curiosity: " + statsRandomizer.curiosity;
            petInfoText[5].text = "Attention Span: " + statsRandomizer.attentionSpan;
            petInfoText[6].text = "Thirst: " + statsRandomizer.thirst;
            petInfoText[7].text = "Hunger: " + statsRandomizer.hunger;
            petInfoText[8].text = "Love: " + statsRandomizer.love;
        }
        else
        {
            if (chosenPetsScript.petsChosen[whichPet] == null)
            {
                print("there is no pet to choose");
                return;
            } //show information about a chosen pet

            currentSelectedPet = whichPet;

            petInfoText[0].text = "Pet Id: " + chosenPetsScript.petsChosen[whichPet].petID;
            string allergensText = "";
            string symptomsText = "";
            for (int index = 0; index < chosenPetsScript.petsChosen[whichPet].allergies.Length; index++)
            {
                allergensText += " - " + chosenPetsScript.petsChosen[whichPet].allergies[index].allergenItem.itemType;
                symptomsText += " - " + chosenPetsScript.petsChosen[whichPet].allergies[index].symptom.symptomType;
            }

            petInfoText[1].text = "Allergens: " + allergensText;
            petInfoText[2].text = "Symptoms: " + symptomsText;
            petInfoText[3].text = "Personality: " + chosenPetsScript.petsChosen[whichPet].personality;
            petInfoText[4].text = "Curiosity: " + chosenPetsScript.petsChosen[whichPet].curiosity;
            petInfoText[5].text = "Attention Span: " + chosenPetsScript.petsChosen[whichPet].attentionSpan;
            petInfoText[6].text = "Thirst: " + chosenPetsScript.petsChosen[whichPet].thirst;
            petInfoText[7].text = "Hunger: " + chosenPetsScript.petsChosen[whichPet].hunger;
            petInfoText[8].text = "Love: " + chosenPetsScript.petsChosen[whichPet].love;
        }
    }
}
