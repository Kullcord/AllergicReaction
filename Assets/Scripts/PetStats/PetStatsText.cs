using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetStatsText : MonoBehaviour
{
    [SerializeField]private PetStatsRandomizer statsRandomizer;
    public TextMeshProUGUI[] petInfoText;
    public Slider[] statSliders;
    //For Showing Chosen Pets
    public ChosenPets chosenPetsScript;
    public TextMeshProUGUI[] petsChosenText;
    public GameObject[] uiObj;//until 2 it's panel info about pets
    public Image[] chosenPetsImg;
    public int currentSelectedPet;
    private void Start()
    {
        for (int i = 0; i < chosenPetsScript.maxPets; i++)
        {
            petsChosenText[i].text = "No pet chosen";
            chosenPetsImg[i].color = Color.white;//show how many available slots your have
        }
    }

    public void UpdateChosenPets()
    {
        for (int i = 0; i < MyPets.petsChosen.Count; i++)
        {
            chosenPetsImg[i].GetComponent<Button>().enabled = true;
            petsChosenText[i].text = MyPets.petsChosen[i].petID + " chosen";
        }
    }
    
    public void DeletePet()
    {
        if (MyPets.petsChosen.Count == 0 || MyPets.petsChosen[currentSelectedPet] == null)
        {
            print("there is no pet to remove");
            return;
        }
        if (MyPets.petsChosen[currentSelectedPet] != null)
        {
            MyPets.petsChosen.Remove(MyPets.petsChosen[currentSelectedPet]);
            petsChosenText[currentSelectedPet].text = "No pet chosen";
            petsChosenText[MyPets.petsChosen.Count].text = "No pet chosen";
            chosenPetsImg[currentSelectedPet].GetComponent<Button>().enabled = false;
            chosenPetsImg[MyPets.petsChosen.Count].GetComponent<Button>().enabled = false;

            UpdateChosenPets();
            
            uiObj[0].SetActive(false);//the whole panel
            uiObj[1].SetActive(true);//Add button
            uiObj[2].SetActive(false);//Remove button
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
            petInfoText[3].text = "Personality: ";
            statSliders[0].value = statsRandomizer.personality;
            petInfoText[4].text = "Curiosity: ";
            statSliders[1].value = statsRandomizer.curiosity;
            petInfoText[5].text = "Attention Span: ";
            statSliders[2].value = statsRandomizer.attentionSpan;
            petInfoText[6].text = "Thirst: ";
            statSliders[3].value = statsRandomizer.thirst;
            petInfoText[7].text = "Hunger: ";
            statSliders[4].value = statsRandomizer.hunger;
            petInfoText[8].text = "Love: ";
            statSliders[5].value = statsRandomizer.love;

        }
        else
        {
            if (MyPets.petsChosen[whichPet] == null)
            {
                print("there is no pet to choose");
                return;
            } //show information about a chosen pet

            currentSelectedPet = whichPet;

            petInfoText[0].text = "Pet Id: " + MyPets.petsChosen[whichPet].petID;
            string allergensText = "";
            string symptomsText = "";
            for (int index = 0; index < MyPets.petsChosen[whichPet].allergies.Length; index++)
            {
                allergensText += " - " + MyPets.petsChosen[whichPet].allergies[index].allergenItem.itemType;
                symptomsText += " - " + MyPets.petsChosen[whichPet].allergies[index].symptom.symptomType;
            }

            petInfoText[1].text = "Allergens: " + allergensText;
            petInfoText[2].text = "Symptoms: " + symptomsText;
            petInfoText[3].text = "Personality: ";
            statSliders[0].value = MyPets.petsChosen[whichPet].personality;
            petInfoText[4].text = "Curiosity: ";
            statSliders[1].value = MyPets.petsChosen[whichPet].curiosity;
            petInfoText[5].text = "Attention Span: ";
            statSliders[2].value = MyPets.petsChosen[whichPet].attentionSpan;
            petInfoText[6].text = "Thirst: ";
            statSliders[3].value = MyPets.petsChosen[whichPet].thirst;
            petInfoText[7].text = "Hunger: ";
            statSliders[4].value = MyPets.petsChosen[whichPet].hunger;
            petInfoText[8].text = "Love: ";
            statSliders[5].value = MyPets.petsChosen[whichPet].love;
            
            //Setting up UI
            uiObj[0].SetActive(true);
            uiObj[1].SetActive(false);
            uiObj[2].SetActive(true);
        }
    }
}
