using UnityEngine;

public class DeveloperTrigger : MonoBehaviour
{
    public string requiredTag = "Bottle";
    public int requiredStep = 1;
    public ClickAreaSpawner controller;

    public float rotateAngle = 30f;
    public float rotateDuration = 1f;
    public int nextStep = 2;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag(requiredTag) && controller != null && controller.CurrentStep() == requiredStep)
        {
            triggered = true;
            controller.TriggerRotation(other.transform, rotateAngle, rotateDuration, nextStep);
        }
    }
}