using UnityEngine;
using UnityEngine.EventSystems;

//Camera needs to have Physics Raycaster

public class ItemDropOnPet : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    void Start()
    {
             
    }

    public void OnDrop(PointerEventData eventData)
    {
        print("on drop on 3D object");

        if (eventData.pointerDrag != null)
        {
            
        }
    }
}
