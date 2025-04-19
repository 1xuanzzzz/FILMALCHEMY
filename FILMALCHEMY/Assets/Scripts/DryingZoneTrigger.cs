using UnityEngine;

public class DryingZoneTrigger : MonoBehaviour
{
    [Header("拖入检测")]
    public string requiredTag = "Photo";  // 拖进来的物体需要设置这个 tag
    public ClickAreaSpawner controller;
    public int requiredStep = 4;
    public int nextStep = 5;

    [Header("显影流程")]
    public PhotoDevelopController photoDevelopController;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag(requiredTag) && controller.CurrentStep() == requiredStep)
        {
            triggered = true;

            // 移除拖拽功能（可选）
            var drag = other.GetComponent<Draggable>();
            if (drag != null)
                Destroy(drag);

            // 启动显影流程
            if (photoDevelopController != null)
            {
                photoDevelopController.StartDevelopment();
                Debug.Log("✅ 拖入托盘 → 启动显影");
            }

            // 隐藏照片对象
            other.gameObject.SetActive(false);

            // 进入下一阶段
            controller.TriggerNextStep(nextStep);
        }
    }
}