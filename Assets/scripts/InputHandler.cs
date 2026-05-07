using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera MainCamera; //camera view
    private bool isDragging = false; // bool variable for checking when active
    private Vector3 dragOffset;
    private Transform draggedObject;


    [SerializeField] private LayerMask draggableLayer;

    private void Awake()
    {
        SetCamera(Camera.main); //sets to where camera is
        Debug.Log(Physics2D.queriesHitTriggers);
        Debug.Log("input Handler");
    }



    public void SetCamera(Camera cam)
    {
        MainCamera = cam;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        if (context.started)
        {
            Ray ray = MainCamera.ScreenPointToRay(mousePosition);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, draggableLayer);

            if (hit.collider != null)
            {
                Debug.Log("Click started on: " + hit.collider.name);
                isDragging = true;
                draggedObject = hit.collider.transform;

                Vector3 mouseWorldPos = MainCamera.ScreenToWorldPoint(mousePosition);
                mouseWorldPos.z = 0f;
                dragOffset = draggedObject.position - mouseWorldPos;
            }
        }
        else if (context.canceled)
        {
            if (isDragging)
            {
                Debug.Log("Drag ended");
                isDragging = false;
                draggedObject = null;
            }
        }
    }

    private void Update()
    {
        if (isDragging && draggedObject != null)
        {

            Vector3 mouseWorldPos = MainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;
            draggedObject.position = mouseWorldPos + dragOffset; // maintain offset
            /* These sounds are a problem for later
            if (draggedObject.tag == "closedBookTutorial" || draggedObject.tag == "openBookTutorial" || draggedObject.tag == "closedBook" || draggedObject.tag == "openBook")
            {
                bookGrabSound.Play();

            }
            if (draggedObject.tag == "watchI" || draggedObject.tag == "watchInMicroscope")
            {
                watchGrabSound.Play();
            }
            */
        }
    }

}