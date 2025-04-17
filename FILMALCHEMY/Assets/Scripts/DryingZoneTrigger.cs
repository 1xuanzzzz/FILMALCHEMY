using UnityEngine;

public class DryingZoneTrigger : MonoBehaviour
{
    public Transform targetObject;
    public ClickAreaSpawner controller;
    public int requiredStep = 4;
    public int nextStep = 5;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.transform == targetObject && controller.CurrentStep() == requiredStep)
        {
            triggered = true;

            var drag = targetObject.GetComponent<Draggable>();
            if (drag != null)
                Destroy(drag);

            targetObject.gameObject.SetActive(false); // ✅ 只隐藏这个最后的物体

            controller.TriggerNextStep(nextStep);
        }
    }
}