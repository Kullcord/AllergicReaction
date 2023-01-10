
using UnityEngine;

public class Item3D : MonoBehaviour
{
    public ItemScriptObj itemObj;
    void Start()
    {
        
    }

    public void AddThisItemToInv()
    {
        Inventory.instance.AddItem(itemObj);
    }

}
