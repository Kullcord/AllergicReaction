using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public bool isOccupied;
    public bool isInQuickBar;

    public GameObject currentItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("on drop");

        if (eventData.pointerDrag != null)
        {
            if (!isOccupied)
            {
                isOccupied = true;
                currentItem = eventData.pointerDrag.gameObject;
                eventData.pointerDrag.transform.SetParent(gameObject.transform); //set the item's parent to this gameobject
                eventData.pointerDrag.GetComponent<GridItem>()._itemSlot = this;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                    GetComponent<RectTransform>().anchoredPosition;
            }
            else
            {
                //switch items between themselves
                //add the item on this slot to the last slot of the dropped item
                GameObject slot1 = eventData.pointerDrag.GetComponent<GridItem>()._itemSlot.gameObject;
                currentItem.GetComponent<GridItem>()._itemSlot = slot1.GetComponent<ItemSlot>();
                slot1.GetComponent<ItemSlot>().currentItem = currentItem;
                currentItem.transform.SetParent(slot1.transform);
                currentItem.GetComponent<RectTransform>().anchoredPosition =
                    slot1.GetComponent<RectTransform>().anchoredPosition;
                slot1.GetComponent<ItemSlot>().isOccupied = true;
                
                
                //add the dropped item into this slot
                isOccupied = true;
                eventData.pointerDrag.transform.SetParent(gameObject.transform); //set the item's parent to this gameobject
                eventData.pointerDrag.GetComponent<GridItem>()._itemSlot = this;
                eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                    GetComponent<RectTransform>().anchoredPosition;
                currentItem = eventData.pointerDrag.gameObject;
            }
            
        }
    }

}
