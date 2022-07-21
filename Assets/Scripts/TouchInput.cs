using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler
{

    private bool isMousePressed;
    private DragData dragData;

    public int raycastDistance;
    public int dragThreshold;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Ray ray = CameraController.instance.mainCamera.ScreenPointToRay(eventData.position);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            VehicleMovementController vehicle = hit.transform.GetComponent<VehicleMovementController>();
            if (vehicle != null && vehicle.vehicleState == VehicleState.In_Parking)
            {
                isMousePressed = true;
                dragData = new DragData{touchPos=eventData.position,vehicle = vehicle};
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isMousePressed = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(!isMousePressed)
            return;

        Vector2 dragVal = dragData.touchPos - eventData.position;

        if (dragVal.magnitude > dragThreshold)
        {
            //attempt vehichle Movement
            //if(dra)
            dragData.vehicle.AttemptToMove(dragVal);
        }
    }
    
    private struct DragData
    {
        public Vector2 touchPos;
        public VehicleMovementController vehicle;
    }
}
