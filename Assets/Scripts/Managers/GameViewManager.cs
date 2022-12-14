using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameViewManager : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject individualCamera;

    public bool switchView = false;//when false then main camera is active/ else individual cam is active

    [SerializeField] private GameObject mainZoneUI;
    [SerializeField] private GameObject individualZoneUI;
    [SerializeField] private IndividualScreenUIManager individualZoneStats;

    public StateManager detectedPet;
    public CharacterStats detectedPetStats;

    private bool doOnce;
    private bool individualScreen;

    private float currentTime = 0.0f;

    private void Start()
    {
        doOnce = true;
    }

    private void Update()
    {
        if (detectedPet == null || detectedPetStats == null || individualCamera == null)
            return;

        ViewManager();

        if (individualScreen)
        {
            if (currentTime <= (detectedPet.stats.atention * 10))
                currentTime += Time.deltaTime * 1;
            else
            {
                switchView = false;
                detectedPet.SwitchToNext(detectedPet.restState);
                detectedPet.exploreState.doOnce = false;
                individualScreen = false;
            }
        }
    }

    private void ViewManager()
    {
        if (switchView)
        {
            doOnce = false;
            if (!doOnce)
            {
                detectedPet.SwitchToNext(detectedPet.individualState);

                mainZoneUI.SetActive(false);
                individualZoneUI.SetActive(true);

                individualZoneStats.SetStats(detectedPetStats.hunger, detectedPetStats.thirst,
                    detectedPetStats.boredome, detectedPetStats.love, detectedPetStats.atention);


                mainCamera.SetActive(false);
                individualCamera.SetActive(true);

                doOnce = true;
                individualScreen = true;
            }
        }
        else
        {
            doOnce = false;
            if (!doOnce)
            {
                individualZoneUI.SetActive(false);
                mainZoneUI.SetActive(true);

                individualZoneStats.SetStats(0, 0, 0, 0, 0);

                individualCamera.SetActive(false);
                mainCamera.SetActive(true);

                currentTime = 0.0f;

                doOnce = true;
            }
        }
    }
}
