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

    [HideInInspector]public bool draggingItem = false;
    [SerializeField] private GameObject randomItem;
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
        Color col = Color.white;
        col.a = 0.5f;
        randomItem.GetComponent<Button>().interactable = false;
        randomItem.GetComponent<Image>().color = col;
    }

    /// <summary>
    /// Adds an item to the inventory and to the grid
    /// </summary>
    /// <param name="itm"></param>
    public void AddItem(ItemSO itm)
    {
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
        
        if (inv.Count == maxInvItems)
        {
            print("Inventory full");
            Color col = Color.white;
            col.a = 0.5f;
            randomItem.GetComponent<Button>().interactable = false;
            randomItem.GetComponent<Image>().color = col;
            return;
        }
    }

    public void RemoveItem(GameObject _item)
    {
        Color col = Color.white;
        col.a = 1f;
        randomItem.GetComponent<Button>().interactable = true;
        randomItem.GetComponent<Image>().color = col;
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
