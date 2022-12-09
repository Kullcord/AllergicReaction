using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private List<GameObject> petList = new List<GameObject>();

    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    private bool isTimeCheckAllowed = true;
    private int leftClickNum = 0;

    private void Awake()
    {
        petList.AddRange(GameObject.FindGameObjectsWithTag("Pet"));
    }

    private void Update()
    {
        DoubleClick();
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



    #endregion

    private RaycastHit CastRay()
    {
        Vector3 screenMousePosFar = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);

        Vector3 worldMousePosFar = Camera.main.ScreenToViewportPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToViewportPoint(screenMousePosNear);

        RaycastHit hit;
        Physics.Raycast(worldMousePosFar, worldMousePosFar - worldMousePosFar, out hit);

        return hit;
    }

    private void Detection(string tag)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == tag)
            {
                Debug.Log("Detected: " + tag);
            }
        }
    }
}
