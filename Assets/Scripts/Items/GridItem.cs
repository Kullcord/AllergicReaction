using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GridItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IInitializePotentialDragHandler
{
    public ItemScriptObj itemObj;

    public bool inStore;
    public bool inQuickBar;

    private RectTransform rectTransform;
    private CanvasGroup cv;
    public ItemSlot _itemSlot;
    private Canvas canv;
    
    void Start()
    {
        Image itemImg = GetComponent<Image>();
        itemImg.sprite = itemObj.itemSprite;
        cv = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        _itemSlot = transform.parent.gameObject.GetComponent<ItemSlot>();
        _itemSlot.currentItem = gameObject;
        float itemW = _itemSlot.gameObject.GetComponent<RectTransform>().rect.width * 0.9f;
        rectTransform.sizeDelta = new Vector2(itemW, itemW);//set item size according to the slot width
        canv = Inventory.instance.transform.parent.GetComponent<Canvas>();
    }
    void Update()
    {
        
    }

    /// <summary>
    /// Used in the store
    /// </summary>
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
        transform.SetParent(canv.transform);
        transform.SetAsLastSibling();
        
        //transform.parent.transform.parent.SetAsLastSibling();
        _itemSlot.isOccupied = false;
        cv.alpha = .6f;
        cv.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("end drag");
        
        transform.SetParent(_itemSlot.transform);
        transform.localPosition = Vector3.zero;
        
        cv.alpha = 1f;
        cv.blocksRaycasts = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
       // rectTransform.anchoredPosition += eventData.delta/canv.scaleFactor;
        transform.position = Input.mousePosition;
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        eventData.useDragThreshold = false;
    }
}
