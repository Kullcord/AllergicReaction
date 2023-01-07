using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public int indicatorID;
    [SerializeField] private StateManager savedPet;
    [SerializeField] private CameraHandler camHolder;

    //List of all pet's in the scene
    [SerializeField] private List<StateManager> pets;


    private void Start()
    {
        pets = new List<StateManager>(FindObjectsOfType<StateManager>());

        foreach(StateManager pet in pets)
        {
            int petID = pet.id;

            if(petID == indicatorID)
            {
                savedPet = pet;
                break;
            }
        }
    }

    public void TeleportToPet()
    {
        var newX = savedPet.gameObject.transform.position.x - camHolder.offsetX;
        var newZ = savedPet.gameObject.transform.position.z + camHolder.offsetZ;
        var newPos = new Vector3(newX, camHolder.transform.position.y, newZ);

        camHolder.cam.transform.position = newPos;
    }
}
