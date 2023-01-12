using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndividualScreenUIManager : MonoBehaviour
{
    public Slider hungerBar;
    public Slider thirstBar;
    public Slider boredomeBar;
    public Slider loveBar;

    private float maxHungerVal = 100;
    private float maxThirstVal = 100;
    private float maxBoredomeVal = 100;
    private float maxLoveVal = 100;

    public float atentionSpan;
    [SerializeField] private float currentTime;
    public Image currentAllergy;

    private void Update()
    {
        if(currentTime <= (atentionSpan * 100))
            currentTime += Time.deltaTime * 5;
        /*else*/

    }

    private void Start()
    {
        hungerBar.maxValue = maxHungerVal;
        thirstBar.maxValue = maxThirstVal;
        boredomeBar.maxValue = maxBoredomeVal;
        loveBar.maxValue = maxLoveVal;
    }
    
    public void SetStats(float hunger, float thirst, float boredome, float love, float atention)
    {
        hungerBar.value = hunger;
        thirstBar.value = thirst;
        boredomeBar.value = boredome;
        loveBar.value = love;

        atentionSpan = atention;
    }

    public void SetAllergenIcon(CharacterStats stats, StateManager manager)
    {
        if (stats.allergicReaction)
        {
            currentAllergy.gameObject.SetActive(true);
                switch (stats.currentReaction.symptom)
                {
                    case Symptoms.Reactions.Itching:
                        switch (stats.currentReaction.allergenItemSo.itemType)
                        {
                            case ItemSO.ItemType.Milk:
                                currentAllergy.sprite = manager.petMenu.Milk[0];
                                break;
                            case ItemSO.ItemType.Wheat:
                                currentAllergy.sprite = manager.petMenu.Wheat[0];
                                break;
                            case ItemSO.ItemType.Peanut:
                                currentAllergy.sprite = manager.petMenu.Peanut[0];
                                break;
                            case ItemSO.ItemType.Cashew:
                                currentAllergy.sprite = manager.petMenu.Cashew[0];
                                break;
                        }

                        break;
                    case Symptoms.Reactions.Wheezing:
                        switch (stats.currentReaction.allergenItemSo.itemType)
                        {
                            case ItemSO.ItemType.Milk:
                                currentAllergy.sprite = manager.petMenu.Milk[1];
                                break;
                            case ItemSO.ItemType.Wheat:
                                currentAllergy.sprite = manager.petMenu.Wheat[1];
                                break;
                            case ItemSO.ItemType.Peanut:
                                currentAllergy.sprite = manager.petMenu.Peanut[1];
                                break;
                            case ItemSO.ItemType.Cashew:
                                currentAllergy.sprite = manager.petMenu.Cashew[1];
                                break;
                        }

                        break;
                    case Symptoms.Reactions.Vomiting:
                        switch (stats.currentReaction.allergenItemSo.itemType)
                        {
                            case ItemSO.ItemType.Milk:
                                currentAllergy.sprite = manager.petMenu.Milk[2];
                                break;
                            case ItemSO.ItemType.Wheat:
                                currentAllergy.sprite = manager.petMenu.Wheat[2];
                                break;
                            case ItemSO.ItemType.Peanut:
                                currentAllergy.sprite = manager.petMenu.Peanut[2];
                                break;
                            case ItemSO.ItemType.Cashew:
                                currentAllergy.sprite = manager.petMenu.Cashew[2];
                                break;
                        }

                        break;
                    case Symptoms.Reactions.Swelling:
                        switch (stats.currentReaction.allergenItemSo.itemType)
                        {
                            case ItemSO.ItemType.Milk:
                                currentAllergy.sprite = manager.petMenu.Milk[3];
                                break;
                            case ItemSO.ItemType.Wheat:
                                currentAllergy.sprite = manager.petMenu.Wheat[3];
                                break;
                            case ItemSO.ItemType.Peanut:
                                currentAllergy.sprite = manager.petMenu.Peanut[3];
                                break;
                            case ItemSO.ItemType.Cashew:
                                currentAllergy.sprite = manager.petMenu.Cashew[3];
                                break;
                        }

                        break;
                    case Symptoms.Reactions.Anaphylaxis:
                        switch (stats.currentReaction.allergenItemSo.itemType)
                        {
                            case ItemSO.ItemType.Milk:
                                currentAllergy.sprite = manager.petMenu.Milk[4];
                                break;
                            case ItemSO.ItemType.Wheat:
                                currentAllergy.sprite = manager.petMenu.Wheat[4];
                                break;
                            case ItemSO.ItemType.Peanut:
                                currentAllergy.sprite = manager.petMenu.Peanut[4];
                                break;
                            case ItemSO.ItemType.Cashew:
                                currentAllergy.sprite = manager.petMenu.Cashew[4];
                                break;
                        }
                        
                        break;
                }
        }
        else
        {
            currentAllergy.gameObject.SetActive(false);
        }
    }


}
