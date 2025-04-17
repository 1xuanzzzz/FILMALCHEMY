using UnityEngine;

public class FixerTrigger : MonoBehaviour
{
    public Transform fixerBottle;
    public ClickAreaSpawner controller;
    public float rotateAngle = 30f;
    public float rotateDuration = 1f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.transform == fixerBottle && controller.CurrentStep() == 2)
        {
            triggered = true;
            Debug.Log("Fixer 进入触发区域！");
            controller.TriggerRotation(fixerBottle, rotateAngle, rotateDuration, 3);
        }
    }
}