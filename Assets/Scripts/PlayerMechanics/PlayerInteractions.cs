using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private CameraHandler camHolder;
    [SerializeField] private Camera cam;
    [SerializeField] private List<GameObject> petList = new List<GameObject>();
    [FormerlySerializedAs("gsm")] [SerializeField] private GameViewManager gvm;

    [Header("Double Click")]
    private float firstLeftClickTime;
    private float timeBetweenLeftClick = 0.5f;
    [SerializeField] private bool isTimeCheckAllowed = true;
    [SerializeField] private int leftClickNum = 0;

    private bool doubleClicked;

    [Header("Drag")]
    public bool isDragging = false;
    [SerializeField] private GameObject selectedObj;

    private float maxTime = 0.1f;
    private float currentTime = 0.0f;
    
    private float maxDragTime = 1.0f;
    private float currentDragTime = 0.0f;

    private bool isDown;
    private bool isHeld;
    private bool drop;

    private Vector3 positionBeforeDrag;


    private void Awake()
    {
        petList.AddRange(GameObject.FindGameObjectsWithTag("Pet"));
    }

    private void Update()
    {
        HandleInput();
    }

    #region Input Handler

    private void HandleInput()
    {
        CheckMouseButtonUp();

        if (Input.GetMouseButtonDown(0))
        {
            currentTime = 0.0f;

            leftClickNum +=1;
            isDown = true;
        }

        if (isDown && Input.GetMouseButton(0))
        {
            currentTime += Time.deltaTime;

            if (currentTime >= maxTime)
            {
                isHeld = true;

                if(!doubleClicked && !camHolder.isMoving)
                    DragPet();
            }
        }

        if (leftClickNum == 1 && isTimeCheckAllowed)
        {
            firstLeftClickTime = Time.time;
            StartCoroutine(DetectDoubleLeftClick());
        }
    }

    #endregion

    #region DoubleClick
    IEnumerator DetectDoubleLeftClick()
    {
        isTimeCheckAllowed = false;
        while(Time.time < firstLeftClickTime + timeBetweenLeftClick)
        {
            if(leftClickNum == 2 && !doOnce)
            {
                doubleClicked = true;
                doOnce = true;

                Detection("Pet");

                break;
            }
            yield return new WaitForEndOfFrame();
        }
        leftClickNum = 0;
        isTimeCheckAllowed = true;
        doubleClicked = false;
    }

    private bool doOnce = false;

    private void Detection(string tag)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.collider.tag == tag)
            {
                var detectedPetStateManager = hit.collider.GetComponent<StateManager>();
                var detectedPetStats = hit.collider.GetComponent<CharacterStats>();

                gvm.individualCamera = detectedPetStateManager.individualCamera;

                gvm.detectedPet = detectedPetStateManager;
                gvm.detectedPetStats = detectedPetStats;

                gvm.switchView = true;

                doOnce = false;
            }
        }
    }

    #endregion

    #region Drag

    private void DragPet()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        if (selectedObj == null)
        {

            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                if (hit.collider != null)
                {
                    if (!hit.collider.CompareTag("Pet"))
                            return;

                    Cursor.visible = false;
                    selectedObj = hit.collider.gameObject;
                }

            }
        }
        //}

        if (selectedObj != null && isHeld)
        {
            currentDragTime += Time.deltaTime;
            var selectedAgent = selectedObj.GetComponent<StateManager>().agent;

            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

            selectedAgent.isStopped = true;
            
            selectedObj.transform.position = new Vector3(worldPosition.x, selectedObj.transform.position.y, worldPosition.z);
            isDragging = true;

            if (currentDragTime >= maxDragTime)
                drop = true;
        }
    }

    #endregion

    #region Mouse Up Behaviour

    private void CheckMouseButtonUp()
    {
        if (Input.GetMouseButtonUp(0) || drop)
        {
            if (selectedObj != null)
            {
                currentDragTime = 0.0f;
                currentTime = 0.0f;

                isDown = false;
                isHeld = false;
                drop = false;

                var selectedAgent = selectedObj.GetComponent<StateManager>().agent;

                Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObj.transform.position).z);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);

                selectedObj.transform.position = new Vector3(worldPosition.x, selectedObj.transform.position.y, worldPosition.z);
                selectedAgent.isStopped = false;

                selectedObj = null;
                Cursor.visible = true;

                Invoke("SetDefault", 0.5f);
                
            }
        }
    }

    private void SetDefault()
    {
        isDragging = false;
    }

    #endregion
}
