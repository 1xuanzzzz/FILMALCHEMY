using UnityEngine;

public class DeveloperTrigger : MonoBehaviour
{
    public Transform developerBottle;
    public ClickAreaSpawner controller;
    public float rotateAngle = 30f;
    public float rotateDuration = 1f;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!triggered && other.transform == developerBottle && controller.CurrentStep() == 1)
        {
            triggered = true;
            Debug.Log("Developer 进入触发区域！");
            controller.TriggerRotation(developerBottle, rotateAngle, rotateDuration, 2);
        }
    }
}