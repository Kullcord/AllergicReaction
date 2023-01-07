using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Props to :https://kylewbanks.com/blog/unity3d-panning-and-pinch-to-zoom-camera-with-touch-and-mouse-input

public class CameraHandler : MonoBehaviour
{
    private static readonly float panSpeed = 10f;
    private static readonly float zoomSpeedTouch = 0.1f;
    private static readonly float zoomSpeedMouse = 10f;

    private static readonly float[] BoundsX = new float[] { -15f, 15f };
    private static readonly float[] BoundsZ = new float[] { -30f, 30f };
    private static readonly float[] ZoomBounds = new float[] { 35f, 75f };

    [SerializeField] private PlayerInteractions player;

    [Header("Offset")]
    public float offsetX;
    public float offsetZ;

    public Camera cam;

    private Vector3 lastPanPosition;
    private int panFingerId; //Touch mode only

    private bool wasZoomingLastFrame;//Touch mode only
    private Vector2[] lastZoomPositions; //Touch mode only

    public bool isMoving;

    private Vector3 prevPos;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        player = GetComponent<PlayerInteractions>();

        prevPos = cam.transform.position;
    }

    private void Update()
    {
        if (player.isDragging)
            return;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
        {
            if (hit.collider != null && hit.collider.CompareTag("Pet"))
                return;
        }

        MovementDetection();

        if (Input.touchSupported && Application.platform != RuntimePlatform.WindowsEditor)
            HandleTouch();
        else
            HandleMouse();
    }

    #region Touch

    void HandleTouch()
    {
        switch (Input.touchCount)
        {
            case 1://Panning
                wasZoomingLastFrame = false;

                //If the touch began, capture its position and its fingers ID;
                //Otherwise, if the finger ID of the touch doesn't match, skip it;
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    lastPanPosition = touch.position;
                    panFingerId = touch.fingerId;
                }
                else if (touch.fingerId == panFingerId && touch.phase == TouchPhase.Moved)
                    PanCamera(touch.position);

                break;

            case 2://Zooming
                Vector2[] newPositions = new Vector2[] {Input.GetTouch(0).position, Input.GetTouch(1).position};
                if (!wasZoomingLastFrame)
                {
                    lastZoomPositions = newPositions;
                    wasZoomingLastFrame = true;
                }
                else
                {
                    //Zoom based on the distance between the new positions compared to the
                    //distance between the previous position
                    float newDistance = Vector3.Distance(newPositions[0], newPositions[1]);
                    float oldDistance = Vector2.Distance(lastZoomPositions[0], lastZoomPositions[1]);
                    float offset = newDistance - oldDistance;

                    ZoomCamera(offset, zoomSpeedTouch);

                    lastZoomPositions = newPositions;
                }

                break;

            default:
                wasZoomingLastFrame = false;
                isMoving = false;
                break;
        }
    }

    #endregion

    #region Mouse

    void HandleMouse()
    {
        //On mouse down, capture it's position.
        //Otherwise, if the mouse is still down, pan the camera;
        if (Input.GetMouseButtonDown(0))
            lastPanPosition = Input.mousePosition;
        else if (Input.GetMouseButton(0))
        {
            PanCamera(Input.mousePosition);
            //Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (Input.GetMouseButtonUp(0))
        {
            //Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isMoving = false;
        }

        //Check for scrolling to zoom the camera.
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        ZoomCamera(scroll, zoomSpeedMouse);
    }

    #endregion

    #region Pan & Zoom

    void PanCamera(Vector3 newPanPosition)
    {
        //Determine how much to move the camera
        Vector3 offset = cam.ScreenToViewportPoint(lastPanPosition - newPanPosition);
        Vector3 move = new Vector3(offset.x * panSpeed, 0, offset.y * panSpeed);

        //Perform the movement
        transform.Translate(-move, Space.World);

        //Ensure the camera remains within bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(transform.position.x, BoundsX[0], BoundsX[1]);
        pos.z = Mathf.Clamp(transform.position.z, BoundsZ[0], BoundsZ[1]);
        transform.position = pos;

        //Cache the position
        lastPanPosition = newPanPosition;
    }

    void ZoomCamera(float offset, float speed)
    {
        if (offset == 0)
            return;

        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView - (offset * speed), ZoomBounds[0], ZoomBounds[1]);
    }

    #endregion

    #region Movement detection

    private void MovementDetection()
    {
        if (prevPos != cam.transform.position)
        {
            prevPos = cam.transform.position;

            isMoving = true;
        }
    }

    #endregion
}
