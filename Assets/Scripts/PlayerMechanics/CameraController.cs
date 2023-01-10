using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cam;
    protected Plane map;

    public float panSmoothing;

    private void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    private void Update()
    {
        //Update Plane
        if (Input.touchCount >= 1)
            map.SetNormalAndPosition(transform.up, transform.position);

        var delta1 = Vector3.zero;
        var delta2 = Vector3.zero;

        //Scroll
        if (Input.touchCount >= 1)
        {
            delta1 = PlanePositionDelta(Input.GetTouch(0));
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                cam.transform.Translate(delta1, Space.World);
            }
        }
    }

    protected Vector3 PlanePositionDelta(Touch touch)
    {
        //not moved
        if (touch.phase != TouchPhase.Moved)
        {
            Debug.Log("Enters1");

            return Vector3.zero;
        }

        //delta
        var rayBefore = cam.ScreenPointToRay(touch.position - touch.deltaPosition);

        Debug.Log("rayBefore: " + rayBefore);

        var rayNow = cam.ScreenPointToRay(touch.position);

        Debug.Log("rayNow: " + rayNow);

        if (map.Raycast(rayBefore, out var enterBefore) && map.Raycast(rayNow, out var enterNow))
        {
            Debug.Log("Enters2");
            return rayBefore.GetPoint(enterBefore) - rayNow.GetPoint(enterNow);

        }

        //not on plane
        return Vector3.zero;
    }

    protected Vector3 PlanePosition(Vector2 screenPos)
    {
        //position
        var rayNow = cam.ScreenPointToRay(screenPos);
        if (map.Raycast(rayNow, out var enterNow))
            return rayNow.GetPoint(enterNow);

        return Vector3.zero;
    }
}
