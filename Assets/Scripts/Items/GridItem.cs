using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GridItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    public ItemSO itemObj;

    public bool inStore;
    public bool inQuickBar;

    [HideInInspector]public RectTransform rectTransform;
    [HideInInspector]public Image itemImg;
    private CanvasGroup cv;
    public ItemSlot _itemSlot;
    private GameObject canv;

    private bool dragging;
    private float currentTime = 0;
    private float maxTime = 5;
    
    void Start()
    {
        cv = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        canv = Inventory.instance.transform.parent.gameObject;
        
        //set in the itemSlot what item it contains
        _itemSlot = transform.parent.gameObject.GetComponent<ItemSlot>();
        _itemSlot.currentItem = gameObject;
        
        //set item sprite
        itemImg = GetComponent<Image>();
        itemImg.sprite = itemObj.itemSprite;
        itemImg.SetNativeSize();

        //set size of the item in the itemSlot
        rectTransform.sizeDelta *= 0.6f;
        
    }

    private void Update()
    {
        if (dragging)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= maxTime)
            {
                EndDrag();
                dragging = false;
            }
        }
    }

    /// <summary>
    /// Used in the store
    /// </summary>
    public void AddThisItemToInv()
    {
        Inventory.instance.AddItem(itemObj);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(canv.transform);
        transform.SetAsLastSibling();
        
        _itemSlot.isOccupied = false;
        cv.alpha = .6f;
        cv.blocksRaycasts = false;
        Inventory.instance.draggingItem = true;
        dragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        EndDrag();
    }

    private void EndDrag()
    {
        transform.SetParent(_itemSlot.transform);
        transform.localPosition = Vector3.zero;
        
        cv.alpha = 1f;
        cv.blocksRaycasts = true;
        Inventory.instance.draggingItem = false;
        dragging = false;
        currentTime = 0;
    }
    public void OnDrag(PointerEventData eventData)
    {
        print("dragging");
        transform.position = Input.mousePosition;
        Inventory.instance.draggingItem = true;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }
}
