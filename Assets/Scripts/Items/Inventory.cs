using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Inventory : MonoBehaviour
{
    public int maxInvItems = 15;
    public List<ItemSO> inv = new List<ItemSO>();

    public List<GameObject> itemSlots = new List<GameObject>(); //add all item grid images

    public static Inventory instance;
    public GameObject invBackground;
    [SerializeField] private GameObject itemSlot;
    [SerializeField] private GameObject itemInSlot;

    [SerializeField] private ItemSO[] itemsToAdd;
    [SerializeField] private ItemSO[] randomItems;

    public bool draggingItem = false;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < itemsToAdd.Length; i++)
        {
            AddItem(itemsToAdd[i]);
        }
    }

    /// <summary>
    /// Adds an item to the inventory and to the grid
    /// </summary>
    /// <param name="itm"></param>
    public void AddItem(ItemSO itm)
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
        Destroy(_item);
    }
    
    //Buttons
    bool invManage ;
    public void OpenCloseInv(GameObject objToToggle)
    {
        invManage = !invManage;
        objToToggle.SetActive(invManage);
    }
    
    public void AddRandomItemToInv()
    {
        ItemSO randomItem = randomItems[Random.Range(0,randomItems.Length)];
        AddItem(randomItem);
    }
}
