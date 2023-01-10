using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetIndicatorsManager : MonoBehaviour
{
    //Prefab for the indicators
    public GameObject indicatorPrefab;

    //List of all pet's in the scene
    [SerializeField] private List<StateManager> pets;

    //List of Pet IDs
    [SerializeField] private List<int> petIDs;

    //List of indicators
    [SerializeField] private List<GameObject> indicators;

    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;

    [SerializeField] private CameraHandler camHolder;

    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private float maxDistance;

    [SerializeField] private AnimationCurve curve;
    private float progression = 0.0f;

    private void Start()
    {
        
        camHolder = FindObjectOfType<CameraHandler>();

        //Find all the pets in the scene
        pets = new List<StateManager>(FindObjectsOfType<StateManager>());

        petIDs = new List<int>();

        //Instantiate an indicator for each NPC
        foreach (StateManager pet in pets)
        {
            //Get the pet ID
            int petID = pet.id;

            //Check if the ID is not already saved, and add the pet ID to the list
            if (!petIDs.Contains(petID))
                petIDs.Add(petID);

            //Create the indicator and add it to the list
            GameObject indicator = Instantiate(indicatorPrefab, transform);
            indicators.Add(indicator);

            indicator.GetComponent<Indicator>().id = petID;
            pet.actionIcon = indicator.GetComponent<Indicator>().actionIcon;

            pet.actionIcon.texture = pet.exploreIcon;

            //indicator.GetComponent<Indicator>().petIcon = petIcon;

            //Add a click event to the indicator button
            Button bttn = indicator.GetComponent<Button>();
            bttn.onClick.AddListener(() => TeleportToPet(pet));
            // Hide the indicator initially
            indicator.SetActive(false);
        }
    }

    private void Update()
    {
        IndicatorBehaviour();
    }

    private void IndicatorBehaviour()
    {
        //Check if each pet is on screen
        for (int i = 0; i < pets.Count; i++)
        {
            StateManager pet = pets[i];
            GameObject indicator = indicators[i];

            //Update the scale of the indicator
            //UpdateIndicatorScale(indicator, pet);

            //If the pet is on screen, turn off its indicator
            if (IsOnScreen(pet.transform.position))
                indicators[i].SetActive(false);

            //If the pet is off screen, turn on its indicator and update its position
            else
            {
                indicators[i].SetActive(true);
                indicators[i].transform.position = GetScreenPosition(pet.transform.position);
            }
        }
    }

    //Returns true if the given world position is on screen
    private bool IsOnScreen(Vector3 worldPosition)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        return screenPosition.x > 0 && screenPosition.x < Screen.width && screenPosition.y > 0 && screenPosition.y < Screen.height;

    }

    //Returns the screen position of the given world position
    private Vector3 GetScreenPosition(Vector3 worldPosition)
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        if (screenPosition.z < 0)
        {
            screenPosition.x = -screenPosition.x;
            screenPosition.y = offsetY;
        }
    
        //Left border
        if (screenPosition.x < offsetX)
            screenPosition.x = offsetX;

        //Right border
        if (screenPosition.x > Screen.width - offsetX)
            screenPosition.x = Screen.width - offsetX;

        //Bottom border
        if (screenPosition.y < offsetY)
            screenPosition.y = offsetY;

        //Top border
        if (screenPosition.y > Screen.height - offsetY)
            screenPosition.y = Screen.height - offsetY;

        return screenPosition;
    }

    private void UpdateIndicatorScale(GameObject indicator, StateManager pet)
    {
        //Calculate the distance between the pet and the camera
        float distance = Vector3.Distance(pet.transform.position, Camera.main.transform.transform.position);

        //Use the distance to determine the scale of the indicator
        float scale = Mathf.Lerp(minScale, maxScale, distance);

        //Set the scale of the indicator
        indicator.transform.localScale = Vector3.one * scale;
    }

    public void TeleportToPet(StateManager pet)
    {
        var newX = pet.gameObject.transform.position.x - camHolder.offsetX;
        var newZ = pet.gameObject.transform.position.z + camHolder.offsetZ;
        var newPos = new Vector3(newX, camHolder.transform.position.y, newZ);

        //camHolder.cam.transform.position = newPos;
        StopAllCoroutines();
        StartCoroutine(CameraLerp(newPos));
    }

    IEnumerator CameraLerp(Vector3 posToGo)
    {
        progression = 0.0f;

        while(progression < 0.99)
        {
            progression += 10 * Time.deltaTime;

            camHolder.cam.transform.position = Vector3.Lerp(camHolder.cam.transform.position, posToGo, curve.Evaluate(progression));

            yield return new WaitForEndOfFrame();
        }
    }
}
