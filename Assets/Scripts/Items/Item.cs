using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class Item : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    public ItemScriptObj itemObj;

    private RectTransform rectTransform;
    private CanvasGroup cv;
    void Start()
    {
        Image itemImg = GetComponent<Image>();
        itemImg.sprite = itemObj.itemSprite;

        rectTransform = GetComponent<RectTransform>();
        cv = GetComponent<CanvasGroup>();
    }
    void Update()
    {
        
    }

    public void AddThisItemToInv()
    {
        Inventory.instance.AddItem(itemObj);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        print("pointer down");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        print("begin drag");
        cv.alpha = .6f;
        cv.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
        cv.alpha = 1f;
        cv.blocksRaycasts = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }
}
