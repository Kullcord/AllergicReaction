using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PetStatsText : MonoBehaviour
{
    [SerializeField]private PetsAndStats stats;
    public int currentSelectedPet; //Current selected out of the pets added

    //For showing traits information
    public Slider[] statSliders;
    
    public GameObject[] uiObj;//until 2 it's panel info about pets/ 3-go to map button
    public Image[] chosenPetsImg; //For visualizing how many pets you can add

    public GameObject[] allergensUI;
    public Color[] allergensUICol;
    [SerializeField] private Sprite availableSlot;
    [SerializeField] private Sprite petSlot;
    private void Start()
    {
        for (int i = 0; i < stats.maxPets; i++)
        {
            chosenPetsImg[i].sprite = availableSlot; //show how many available slots your have
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
            chosenPetsImg[i].sprite = petSlot;
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
            chosenPetsImg[currentSelectedPet].sprite = availableSlot;
            chosenPetsImg[MyPets.petsChosen.Count].sprite = availableSlot;
            chosenPetsImg[currentSelectedPet].GetComponent<Button>().enabled = false;
            chosenPetsImg[MyPets.petsChosen.Count].GetComponent<Button>().enabled = false;

            VisuallyUpdateChosenPets();
            
            //Setting up UI
            uiObj[1].SetActive(true);//Add button
            uiObj[2].SetActive(false);//Remove button
        }
    }
    
    /// <summary>
    /// Deletes all data about all pets and starts new
    /// </summary>
    /// <returns></returns>
    public void ResetStats()
    {
        for (int i = 0; i < MyPets.petsChosen.Count; i++)
        {
            chosenPetsImg[i].sprite = availableSlot;
            chosenPetsImg[i].GetComponent<Button>().enabled = false;
        } 
        MyPets.petsChosen.Clear();
    }
    /// <summary>
    /// Sets up the text to show traits information about the pet
    /// </summary>
    /// <param name="whichPet">Which pet was chosen out of the ones added/ write 10 if it's randomly generated pet</param>
    public void SetUpTextInfo(int whichPet = 10)
    {
        if (whichPet == 10) //show info about a randomized pet
        {
            for (int i = 0; i < allergensUI.Length; i++)
            {
                allergensUI[i].SetActive(false);
            }
            //allergens
            for (int i = 0; i < stats.randomPet.allergies.Length; i++)
            {
                allergensUI[i].SetActive(true);
                allergensUI[i].GetComponent<Image>().sprite =
                    stats.randomPet.allergies[i].allergenItemScriptObj.allergenSprite;
                switch (stats.randomPet.allergies[i].symptom)
                {
                    case Symptoms.Reactions.Itching:
                        statSliders[i + 3].value = 1;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[0];
                        break;
                    case Symptoms.Reactions.Wheezing:
                        statSliders[i+3].value = 2;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[1];
                        break;
                    case Symptoms.Reactions.Vomiting:
                        statSliders[i+3].value = 3;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[2];
                        break;
                    case Symptoms.Reactions.Swelling:
                        statSliders[i + 3].value = 4;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[3];
                        break;
                    case Symptoms.Reactions.Anaphylaxis:
                        statSliders[i + 3].value = 5;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[4];
                        break;
                }
            }
            
            //energy
            statSliders[0].value = stats.randomPet.energy;
            //curiosity
            statSliders[1].value = stats.randomPet.curiosity;
            //attention span
            statSliders[2].value = stats.randomPet.attentionSpan;

        }
        else //Show info about an added pet you chose to read about
        {
            if (MyPets.petsChosen[whichPet] == null)
            {
                print("there is no pet to choose");
                return;
            }

            currentSelectedPet = whichPet;

            string allergensText = "";
            string symptomsText = "";
            for (int index = 0; index < MyPets.petsChosen[whichPet].allergies.Length; index++)
            {
                allergensText += " - " + MyPets.petsChosen[whichPet].allergies[index].allergenItemScriptObj.itemType;
                symptomsText += " - " + MyPets.petsChosen[whichPet].allergies[index].symptom;
            }
            for (int i = 0; i < allergensUI.Length; i++)
            {
                allergensUI[i].SetActive(false);
            }
            //allergens
            for (int i = 0; i < MyPets.petsChosen[whichPet].allergies.Length; i++)
            {
                allergensUI[i].SetActive(true);
                allergensUI[i].GetComponent<Image>().sprite =
                    MyPets.petsChosen[whichPet].allergies[i].allergenItemScriptObj.allergenSprite;
                switch ( MyPets.petsChosen[whichPet].allergies[i].symptom)
                {
                    case Symptoms.Reactions.Itching:
                        statSliders[i + 3].value = 1;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[0];
                        break;
                    case Symptoms.Reactions.Wheezing:
                        statSliders[i+3].value = 2;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[1];
                        break;
                    case Symptoms.Reactions.Vomiting:
                        statSliders[i+3].value = 3;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[2];
                        break;
                    case Symptoms.Reactions.Swelling:
                        statSliders[i + 3].value = 4;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[3];
                        break;
                    case Symptoms.Reactions.Anaphylaxis:
                        statSliders[i + 3].value = 5;
                        statSliders[i + 3].transform.GetChild(1).GetChild(0).GetComponent<Image>().color = allergensUICol[4];
                        break;
                }
            }
            
            
            statSliders[0].value = MyPets.petsChosen[whichPet].energy;
            statSliders[1].value = MyPets.petsChosen[whichPet].curiosity;
            statSliders[2].value = MyPets.petsChosen[whichPet].attentionSpan;
            
            
            
            //Setting up UI
            uiObj[1].SetActive(false);
            uiObj[2].SetActive(true);
        }
    }
}
