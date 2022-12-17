using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAvailability : MonoBehaviour
{
    public bool inUse = false;
    public int id;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pet"))
        {
            id = other.gameObject.GetComponent<CharacterStats>().petID;

            inUse = true;
        }
    }
}
