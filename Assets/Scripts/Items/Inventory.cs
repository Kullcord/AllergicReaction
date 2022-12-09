using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int maxInvItems = 15;
    public List<ItemScriptObj> inv = new List<ItemScriptObj>();

    public List<Image> itemGrid = new List<Image>(); //add all item grid images

    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddItem(ItemScriptObj itm)
    {
        if (inv.Count == maxInvItems)
        {
            print("Inventory full");
            return;
        }
        
        inv.Add(itm);
        
        //add item to the inventory grid
        for (int i = 0; i < inv.Count; i++)
        {
            itemGrid[i].gameObject.SetActive(true);
            
            itemGrid[inv.Count - 1].sprite = itm.itemSprite;
            itemGrid[inv.Count - 1].GetComponent<Item>().itemObj = itm;
        }
    }
}
