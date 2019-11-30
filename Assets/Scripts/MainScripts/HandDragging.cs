using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class HandDragging : MonoBehaviour, IManipulationHandler
{
    [SerializeField]
    float DragSpeed = 1.5f;

    [SerializeField]
    float DragScale = 1.5f;

    [SerializeField]
    float MaxDragDistance = 3f;
        
    Vector3 lastPosition;

    [SerializeField]
    bool draggingEnabled = true;
    public void SetDragging(bool enabled)
    {
        draggingEnabled = enabled;
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        InputManager.Instance.PushModalInputHandler(gameObject);
        lastPosition = transform.position;
        gameObject.GetComponent<Rigidbody>().useGravity = false;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        if (draggingEnabled)
        {         
            Drag(eventData.CumulativeDelta);
            //sharing & messaging
            //SharingMessages.Instance.SendDragging(Id, eventData.CumulativeDelta);
        }
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
        gameObject.GetComponent<Rigidbody>().useGravity = true;
        GameObject.Find("ScriptManager").GetComponent<PrimaryScript>().HasBeenMoved = true;
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        InputManager.Instance.PopModalInputHandler();
        gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    void Drag(Vector3 positon)
    {
        var targetPosition = lastPosition + positon * DragScale;
        if (Vector3.Distance(lastPosition, targetPosition) <= MaxDragDistance)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, DragSpeed);
        }
    }
}
