using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item3D : MonoBehaviour
{
    public ItemScriptObj itemObj;
    void Start()
    {
        
    }

    public void AddThisItemToInv()
    {
        Inventory.instance.AddItem(itemObj);
    }

}
