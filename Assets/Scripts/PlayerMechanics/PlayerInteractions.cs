using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private List<GameObject> petList = new List<GameObject>();

    [Header("Double Click")]
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    [SerializeField] private bool isTimeCheckAllowed = true;
    [SerializeField] private int leftClickNum = 0;

    [Header("Drag")]
    public bool isDragging = false;
    [SerializeField] private GameObject selectedObj;

    private void Awake()
    {
        petList.AddRange(GameObject.FindGameObjectsWithTag("Pet"));
    }

    private void Update()
    {
        DoubleClick();

        DragPet();
    }

    #region DoubleClick

    private void DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
            leftClickNum+=1;
        if(leftClickNum == 1 && isTimeCheckAllowed)
        {
            firstLeftClickTime = Time.time;
            StartCoroutine(DetectDoubleLeftClick());
        }
    }

    IEnumerator DetectDoubleLeftClick()
    {
        isTimeCheckAllowed = false;
        while(Time.time < firstLeftClickTime + timeBetweenLeftClick)
        {
            if(leftClickNum == 2)
            {
                Detection("Pet");

                break;
            }
            yield return new WaitForEndOfFrame();
        }
        leftClickNum = 0;
        isTimeCheckAllowed = true;
    }

    #endregion

    #region Drag

    private void DragPet()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (selectedObj == null)
            {
                RaycastHit hit;

                if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    if (hit.collider != null)
                    {
                        if (!hit.collider.CompareTag("Pet"))
                                return;

                        selectedObj = hit.collider.gameObject;
                        Cursor.visible = false;
                    }

                }

            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if(selectedObj != null)
            {
                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

                selectedObj.transform.position = new Vector3(worldPosition.x, 0f, worldPosition.z);

                selectedObj = null;
                Cursor.visible = true;
                isDragging = false;
            }
        }

        if (selectedObj != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

            selectedObj.transform.position = new Vector3(worldPosition.x, .25f, worldPosition.z);
            isDragging = true;
        }
    }


    #endregion

    private void Detection(string tag)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == tag)
            {
                Debug.Log("Detected: " + tag);

                Debug.Log("Go to 1-on-1 screen");
            }
        }
    }
}
