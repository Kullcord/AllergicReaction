using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetStatsText : MonoBehaviour
{
    [SerializeField]private PetsAndStats stats;
    public int currentSelectedPet; //Current selected out of the pets added

    //For showing traits information
    public TextMeshProUGUI[] petInfoText;
    public Slider[] statSliders;
    
    public TextMeshProUGUI[] petsChosenText; //For Showing Chosen Pets on the boxes
    public GameObject[] uiObj;//until 2 it's panel info about pets/ 3-go to map button
    public Image[] chosenPetsImg; //For visualizing how many pets you can add
  
    private void Start()
    {
        for (int i = 0; i < stats.maxPets; i++)
        {
            petsChosenText[i].text = "No pet chosen";
            chosenPetsImg[i].color = Color.white; //show how many available slots your have
        }
    }
    
    /// <summary>
    /// Update the box with chosen pets whenever it is changed
    /// </summary>
    public void VisuallyUpdateChosenPets()
    {
        for (int i = 0; i < MyPets.petsChosen.Count; i++)
        {
            chosenPetsImg[i].GetComponent<Button>().enabled = true;
            petsChosenText[i].text = MyPets.petsChosen[i].petID + " chosen";
        }

        //Show Choose Map Button if you have chosen at least 1 pet 
        if (MyPets.petsChosen.Count != 0)
        {
            uiObj[3].SetActive(true);
        }
        else
        {
            uiObj[3].SetActive(false);
        }
    }
    
    /// <summary>
    /// Remove a chosen pet
    /// </summary>
    public void RemovePet()
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

            VisuallyUpdateChosenPets();
            
            //Setting up UI
            uiObj[0].SetActive(false);//the whole panel
            uiObj[1].SetActive(true);//Add button
            uiObj[2].SetActive(false);//Remove button
        }
    }
    /// <summary>
    /// Sets up the text to show traits information about the pet
    /// </summary>
    /// <param name="whichPet">Which pet was chosen out of the ones added/ write 10 if it's randomly generated pet</param>
    public void SetUpTextInfo(int whichPet = 10)
    {
        if (whichPet == 10) //show info about a randomized pet
        {
            
            petInfoText[0].text = "Pet Id: " + stats.randomPet.petID;
            string allergensText = "";
            string symptomsText = "";
            //in the text show all types of allergens and symptoms
            for (int index = 0; index < stats.randomPet.allergies.Length; index++)
            {
                allergensText += " - " + stats.randomPet.allergies[index].allergenItemScriptObj.itemType;
                symptomsText += " - " + stats.randomPet.allergies[index].symptom;
            }

            petInfoText[1].text = "Allergens: " + allergensText;
            petInfoText[2].text = "Symptoms: " + symptomsText;
            
            petInfoText[3].text = "Personality: ";
            statSliders[0].value = stats.randomPet.personality;
            
            petInfoText[4].text = "Curiosity: ";
            statSliders[1].value = stats.randomPet.curiosity;
            
            petInfoText[5].text = "Attention Span: ";
            statSliders[2].value = stats.randomPet.attentionSpan;
            
            petInfoText[6].text = "Thirst: ";
            statSliders[3].value = stats.randomPet.thirst;
            
            petInfoText[7].text = "Hunger: ";
            statSliders[4].value = stats.randomPet.hunger;
            
            petInfoText[8].text = "Love: ";
            statSliders[5].value = stats.randomPet.love;

        }
        else //Show info about an added pet you chose to read about
        {
            if (MyPets.petsChosen[whichPet] == null)
            {
                print("there is no pet to choose");
                return;
            }

            currentSelectedPet = whichPet;

            petInfoText[0].text = "Pet Id: " + MyPets.petsChosen[whichPet].petID;
            string allergensText = "";
            string symptomsText = "";
            for (int index = 0; index < MyPets.petsChosen[whichPet].allergies.Length; index++)
            {
                allergensText += " - " + MyPets.petsChosen[whichPet].allergies[index].allergenItemScriptObj.itemType;
                symptomsText += " - " + MyPets.petsChosen[whichPet].allergies[index].symptom;
            }

            petInfoText[1].text = "Allergens: " + allergensText;
            petInfoText[2].text = "Symptoms: " + symptomsText;
            
            statSliders[0].value = MyPets.petsChosen[whichPet].personality;
            statSliders[1].value = MyPets.petsChosen[whichPet].curiosity;
            statSliders[2].value = MyPets.petsChosen[whichPet].attentionSpan;
            statSliders[3].value = MyPets.petsChosen[whichPet].thirst;
            statSliders[4].value = MyPets.petsChosen[whichPet].hunger;
            statSliders[5].value = MyPets.petsChosen[whichPet].love;
            
            //Setting up UI
            uiObj[0].SetActive(true);
            uiObj[1].SetActive(false);
            uiObj[2].SetActive(true);
        }
    }
}
