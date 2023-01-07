using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUIManager : MonoBehaviour
{
    #region Fields
    [SerializeField] private int id;

    [Header("Refferences")]
    [SerializeField] private GameObject petTab;
    [SerializeField] private GameObject statsTab;
    [SerializeField] private StateManager pet;
    [SerializeField] private CharacterStats petStats;
    [SerializeField] private CameraHandler camHolder;

    [Header("Action")]
    public RawImage actionIcon;

    [Header("Action Textures")]
    public Texture exploreIcon;
    public Texture digIcon;
    public Texture smellIcon;
    public Texture playIcon;
    public Texture needsIcon;

    [Header("Bars")]
    [SerializeField] private Slider hungerBar;
    [SerializeField] private Slider thirstBar;
    [SerializeField] private Slider boredomeBar;
    [SerializeField] private Slider loveBar;

    private float maxHungerValue;
    private float maxThirstValue;
    private float maxBoredomeValue;
    private float maxLoveValue;

    [Header("Navigation")]
    [SerializeField] private GameObject navIcon;
    [SerializeField] private float offset;

    [Header("Tabs")]
    [SerializeField] private Button petTabButton;
    [SerializeField] private Button statsTabButton;
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    [SerializeField] private bool petTabActive = false;
    [SerializeField] private bool statsTabActive = false;
    #endregion

    //need to somehow get the character stats and the state machine with the same id as this obj
    private void Start()
    {
        if (id != pet.id)
        {
            Debug.LogError("StatsMenu ID: " + id + " does not match AI id: " + pet.id);
            return;
        }

        if(id != petStats.petID)
        {
            Debug.LogError("StatsMenu ID: " + id + " does not match Character id: " + pet.id);
            return;
        }

        maxHungerValue = petStats.hunger;
        hungerBar.maxValue = maxHungerValue;
        hungerBar.value = maxHungerValue;

        maxThirstValue = petStats.thirst;
        thirstBar.maxValue = maxThirstValue;
        thirstBar.value = maxThirstValue;

        maxBoredomeValue = petStats.boredome;
        boredomeBar.maxValue = maxBoredomeValue;
        boredomeBar.value = maxBoredomeValue;

        maxLoveValue = petStats.love;
        loveBar.maxValue = maxLoveValue;
        loveBar.value = maxLoveValue;

        petTabActive = true;
    }

    private void Update()
    {
        if (statsTab.activeSelf)
        {
            StatsBarManager();
        }

        if(petTab.activeSelf)
            RotateNavIcon();
    }

    private void StatsBarManager()
    {
        hungerBar.value = petStats.hunger;

        thirstBar.value = petStats.thirst;

        boredomeBar.value = petStats.boredome;

        loveBar.value = petStats.love;
    }

    private void RotateNavIcon()
    {
        //Get the pos of the obj in screen space
        Vector3 objScreenPos = Camera.main.WorldToScreenPoint(pet.transform.position);

        if(objScreenPos.z < 0)
        {
            objScreenPos.x = -objScreenPos.x;
            objScreenPos.y = -objScreenPos.y;
            objScreenPos.z = -objScreenPos.z;
        }

        //Get the directional vector between the arrow and the pet
        Vector3 dir = (objScreenPos - navIcon.transform.position).normalized;

        //Calculate angle
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.right));

        //Use the cross product to determine if the angle is clockwise or not
        Vector3 cross = Vector3.Cross(dir, Vector3.right);
        angle = -Mathf.Sign(cross.z) * angle;

        //Update the rotation of your arrow
        navIcon.transform.localEulerAngles = new Vector3(navIcon.transform.localEulerAngles.x, navIcon.transform.localEulerAngles.y, angle);
    }

    public void TeleportToPet()
    {
        var newX = pet.gameObject.transform.position.x - camHolder.offsetX; 
        var newZ = pet.gameObject.transform.position.z + camHolder.offsetZ;
        var newPos = new Vector3(newX, camHolder.transform.position.y, newZ);

        camHolder.cam.transform.position = newPos;
    }

    #region Tab functionality

    public void PetTab()
    {
        if (!petTabActive)
        {
            petTabButton.image.sprite = activeSprite;
            statsTabButton.image.sprite = inactiveSprite;

            petTab.SetActive(true);
            statsTab.SetActive(false);

            statsTabActive = false;
            petTabActive = true;
        }
        else
            return;
    }

    public void StatsTab()
    {
        if (!statsTabActive)
        {
            petTabButton.image.sprite = inactiveSprite;
            statsTabButton.image.sprite = activeSprite;

            petTab.SetActive(false);
            statsTab.SetActive(true);

            statsTabActive = true;
            petTabActive = false;
        }
        else
            return;
    }

    #endregion
}
