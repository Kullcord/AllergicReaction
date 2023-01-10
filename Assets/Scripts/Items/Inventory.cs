using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int maxInvItems = 15;
    public List<ItemScriptObj> inv = new List<ItemScriptObj>();

    public List<GameObject> itemSlots = new List<GameObject>(); //add all item grid images

    public static Inventory instance;
    public GameObject invBackground;
    [SerializeField] private GameObject itemSlot;
    [SerializeField] private GameObject itemInSlot;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// Adds an item to the inventory and to the grid
    /// </summary>
    /// <param name="itm"></param>
    public void AddItem(ItemScriptObj itm)
    {
        if (inv.Count == maxInvItems)
        {
            print("Inventory full");
            return;
        }

        inv.Add(itm);

        //find the first slot available
        ItemSlot availableSlot = null;
        for (int i = 0; i < itemSlots.Count; i++)
        {
            var thisSlot = itemSlots[i].GetComponent<ItemSlot>();

            if (thisSlot.isOccupied) continue;
            availableSlot = thisSlot;
            break; // Stop the loop
        }

        if (availableSlot == null)
        {
            print("Available Slot is null");
            return;
        }
        
        //add item to the slot
        availableSlot.isOccupied = true;
        GameObject itemClone = Instantiate(itemInSlot, availableSlot.transform);
        itemClone.GetComponent<GridItem>().itemObj = itm;
    }

    public void RemoveItem(GameObject _item)
    {
        inv.Remove(_item.GetComponent<GridItem>().itemObj);
        
    }
    
    //Buttons
    bool invManage ;
    public void OpenCloseInv()
    {
        invManage = !invManage;
        gameObject.SetActive(invManage);
    }
}
