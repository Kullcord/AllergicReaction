
using UnityEngine;

public class Item3D : MonoBehaviour
{
    public ItemSO itemObj;
    void Start()
    {
        
    }

    public void AddThisItemToInv()
    {
        Inventory.instance.AddItem(itemObj);
    }

}
