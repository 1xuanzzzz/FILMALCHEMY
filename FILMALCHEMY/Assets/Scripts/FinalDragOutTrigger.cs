using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDragOutTrigger : MonoBehaviour
{
    [Header("检测拖入的对象")]
    public string requiredTag = "PhotoFinish";
    public int requiredStep = 7;

    [Header("显示隐藏物体")]
    public GameObject objectToHide;
    public GameObject objectToShow;

    [Header("场景切换")]
    public string nextSceneName = "NextScene";
    public float delayBeforeSceneLoad = 5f;

    [Header("流程控制器（可选）")]
    public ClickAreaSpawner controller;

    [Header("场景跳转控制器")]
    public SceneFlowController sceneFlowController;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag(requiredTag))
        {
            if (controller != null && controller.CurrentStep() != requiredStep)
                return;

            triggered = true;
            Debug.Log("✅ 带有 PhotoFinish 标签的物体已进入触发区，执行流程");

            if (sceneFlowController != null)
            {
                sceneFlowController.StartSceneTransition(objectToHide, objectToShow, delayBeforeSceneLoad, nextSceneName);
            }
            else
            {
                Debug.LogWarning("❌ 未设置 SceneFlowController，无法切换场景");
            }
        }
    }
}
