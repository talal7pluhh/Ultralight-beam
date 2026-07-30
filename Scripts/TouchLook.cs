using UnityEngine;
using UnityEngine.EventSystems;
public class TouchLook : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public Transform PlayerBody;
    public Transform CameraTransform;

    public float sensitivity = 0.2f;
    private float Xrotation = 0f;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        // this function is declared to only sense the touch 
    }
    public void OnDrag(PointerEventData eventData)
    {
        float mouseX = eventData.delta.x * sensitivity;
        float mouseY = eventData.delta.y * sensitivity;

        PlayerBody.Rotate(Vector3.up * mouseX);

        Xrotation -= mouseY;
        Xrotation = Mathf.Clamp(Xrotation, -90f, 90f);

        CameraTransform.localRotation = Quaternion.Euler(Xrotation, 0f, 0f);
    }
}
