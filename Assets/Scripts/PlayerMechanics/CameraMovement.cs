using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerInteractions player;
    [SerializeField] private Camera cam;

    [SerializeField] private float zoomStep;
    [SerializeField] private float minCamSize;
    [SerializeField] private float maxCamSize;

    [SerializeField] private MeshRenderer mapRend;

    private float mapMinX;
    private float mapMaxX;
    private float mapMinZ;
    private float mapMaxZ;

    private Vector3 dragOrigin;

    private void Awake()
    {
        mapMinX = mapRend.transform.position.x - mapRend.bounds.size.x / 2f;
        mapMaxX = mapRend.transform.position.x + mapRend.bounds.size.x / 2f;

        mapMinZ = mapRend.transform.position.z - mapRend.bounds.size.z / 2f;
        mapMaxZ = mapRend.transform.position.z + mapRend.bounds.size.z / 2f;
    }

    private void Start()
    {
        player.GetComponent<PlayerInteractions>();
    }

    private void Update()
    {
        if(!player.isDragging)
            MoveCamera();
    }

    private void MoveCamera()
    {
        //save position of mouse in world space when drag starts(first time clicked)

        if (Input.GetMouseButtonDown(0)) 
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);

        //calculate distance between drag origin and new position if it is still held down
        if (Input.GetMouseButton(0))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            //move the camera by that distance
            cam.transform.position = ClampCamera(cam.transform.position + difference);
        }
    }

    public void ZoomIn()
    {
        float newSize = cam.orthographicSize - zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    public void ZoomOut()
    {
        float newSize = cam.orthographicSize + zoomStep;

        cam.orthographicSize = Mathf.Clamp(newSize, minCamSize, maxCamSize);

        cam.transform.position = ClampCamera(cam.transform.position);
    }

    private Vector3 ClampCamera(Vector3 targetPos)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minZ = mapMinZ + camHeight;
        float maxZ = mapMaxZ - camHeight;

        float newX = Mathf.Clamp(targetPos.x, minX, maxX);
        float newZ = Mathf.Clamp(targetPos.z, minZ, maxZ);

        return new Vector3(newX, targetPos.y, newZ);
    }
}
