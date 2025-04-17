using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;

    public int requiredStep = 1;
    public ClickAreaSpawner controller;

    private void Start()
    {
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        if (controller != null && controller.CurrentStep() != requiredStep) return;
        offset = transform.position - GetMouseWorldPoint();
    }

    void OnMouseDrag()
    {
        if (controller != null && controller.CurrentStep() != requiredStep) return;
        transform.position = GetMouseWorldPoint() + offset;
    }

    Vector3 GetMouseWorldPoint()
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = 10f;
        return cam.ScreenToWorldPoint(screenPoint);
    }
}