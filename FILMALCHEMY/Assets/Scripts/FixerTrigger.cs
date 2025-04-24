using UnityEngine;

public class FixerTrigger : MonoBehaviour
{
    public string requiredTag = "Container";
    public int requiredStep = 2;
    public ClickAreaSpawner controller;

    public float rotateAngle = 30f;
    public float rotateDuration = 1f;
    public int nextStep = 3;
    public Collider2D RangeCollider;
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag(requiredTag) && controller != null && controller.CurrentStep() == requiredStep)
        {
            triggered = true;
            controller.TriggerRotation(other.transform, rotateAngle, rotateDuration, nextStep);
            RangeCollider.gameObject.SetActive(false);
        }
    }
}