using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public bool isOccupied;
    public bool isInQuickBar;

    public GameObject currentItem;
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
                RectTransform rectT = eventData.pointerDrag.GetComponent<RectTransform>();
                rectT.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.GetComponent<GridItem>().itemImg.SetNativeSize();
                if (isInQuickBar)
                    rectT.sizeDelta *= 0.5f;
                else
                    rectT.sizeDelta *= 0.6f;
            }
            else
            {
                //switch items between themselves
                //add the item on this slot to the last slot of the dropped item
                GameObject slot1 = eventData.pointerDrag.GetComponent<GridItem>()._itemSlot.gameObject;
                ItemSlot slot1Script = slot1.GetComponent<ItemSlot>();
                currentItem.GetComponent<GridItem>()._itemSlot = slot1Script;
                slot1Script.currentItem = currentItem;
                currentItem.transform.SetParent(slot1.transform);
                RectTransform currItemRect = currentItem.GetComponent<RectTransform>();
                currItemRect.anchoredPosition = slot1.GetComponent<RectTransform>().anchoredPosition;
                slot1Script.isOccupied = true;
                
                currItemRect.GetComponent<GridItem>().itemImg.SetNativeSize();
                if (slot1Script.isInQuickBar)
                    currItemRect.sizeDelta *= 0.5f;
                else
                    currItemRect.sizeDelta *= 0.6f;
                
                //add the dropped item into this slot
                isOccupied = true;
                eventData.pointerDrag.transform.SetParent(gameObject.transform); //set the item's parent to this gameobject
                eventData.pointerDrag.GetComponent<GridItem>()._itemSlot = this;
                RectTransform rectTDropped = eventData.pointerDrag.GetComponent<RectTransform>();
                rectTDropped.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                currentItem = eventData.pointerDrag.gameObject;
                
                eventData.pointerDrag.GetComponent<GridItem>().itemImg.SetNativeSize();
                if (isInQuickBar)
                    rectTDropped.sizeDelta *= 0.5f;
                else
                    rectTDropped.sizeDelta *= 0.6f;
            }
        }
    }
}
