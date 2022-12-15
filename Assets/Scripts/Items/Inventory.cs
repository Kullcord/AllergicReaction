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

    private void Start()
    {
       StartCoroutine(InstantiateSlots());
        
    }

    private void Update()
    {
        //Testing purposes
        if (Input.GetKeyDown(KeyCode.A))
        {
            float width = invBackground.GetComponent<RectTransform>().rect.width;
            float height = invBackground.GetComponent<RectTransform>().rect.height;
            GridLayoutGroup grid = invBackground.GetComponent<GridLayoutGroup>();
            int column;
            int row;
            GetColumnAndRow(grid, out column, out row);
            grid.cellSize = new Vector2(width / row - 15, height / column - 15);
        }
    }

    /// <summary>
    /// Instantiates all item slots and set their size according to their parent
    /// </summary>
    /// <returns></returns>
    IEnumerator InstantiateSlots()
    {
        bool done = false;
        //instantiate all item slots 
        for (int i = 0; i < maxInvItems; i++)
        {
            GameObject itemSlotClone = Instantiate(itemSlot, invBackground.transform);
            itemSlots.Add(itemSlotClone.gameObject.transform.GetChild(0).gameObject);

            if (i == maxInvItems - 1) done = true;
        }

        yield return new WaitUntil(() => done);
        float width = invBackground.GetComponent<RectTransform>().rect.width;
        float height = invBackground.GetComponent<RectTransform>().rect.height;
        GridLayoutGroup grid = invBackground.GetComponent<GridLayoutGroup>();
        int column;
        int row;
        GetColumnAndRow(grid, out column, out row);
        grid.cellSize = new Vector2(width / row - 15, height / column - 15);
        yield return new WaitForSeconds(1);
        grid.enabled = false;
    }
    
    /// <summary>
    /// Gets the amount of columns and rows in a grid layout group
    /// </summary>
    /// <param name="glg"></param>
    /// <param name="column"></param>
    /// <param name="row"></param>
    void GetColumnAndRow(GridLayoutGroup glg, out int column, out int row)
    {
        column = 0;
        row = 0;

        if (glg.transform.childCount == 0)
            return;

        //Column and row are now 1
        column = 1;
        row = 1;

        //Get the first child GameObject of the GridLayoutGroup
        RectTransform firstChildObj = glg.transform.
            GetChild(0).GetComponent<RectTransform>();

        Vector2 firstChildPos = firstChildObj.anchoredPosition;
        bool stopCountingRow = false;

        //Loop through the rest of the child object
        for (int i = 1; i < glg.transform.childCount; i++)
        {
            //Get the next child
            RectTransform currentChildObj = glg.transform.
                GetChild(i).GetComponent<RectTransform>();

            Vector2 currentChildPos = currentChildObj.anchoredPosition;

            //if first child.x == otherchild.x, it is a column, ele it's a row
            if (firstChildPos.x == currentChildPos.x)
            {
                column++;
                //Stop couting row once we find column
                stopCountingRow = true;
            }
            else
            {
                if (!stopCountingRow)
                    row++;
            }
        }
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
        GameObject itemClone;
        
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
        itemClone = Instantiate(itemInSlot, availableSlot.gameObject.transform);
        itemClone.GetComponent<Image>().sprite = itm.itemSprite;
        itemClone.GetComponent<GridItem>().itemObj = itm;
    }
}
