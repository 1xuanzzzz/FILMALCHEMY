using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDragOutTrigger : MonoBehaviour
{
    [Header("检测拖入的对象")]
    public string requiredTag = "PhotoFinish";  // ✅ 只允许带这个Tag的物体触发
    public int requiredStep = 7;                // 当前步骤限制

    [Header("显示隐藏物体")]
    public GameObject objectToHide;
    public GameObject objectToShow;

    [Header("场景切换")]
    public string nextSceneName = "NextScene";
    public float delayBeforeSceneLoad = 5f;

    [Header("流程控制器（可选）")]
    public ClickAreaSpawner controller;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        // ✅ 检查 Tag 和 Step 阶段
        if (other.CompareTag(requiredTag))
        {
            if (controller != null && controller.CurrentStep() != requiredStep)
                return;

            triggered = true;
            Debug.Log("✅ 带有 PhotoFinish 标签的物体已进入触发区，执行流程");

            StartCoroutine(HandleSuccess());
        }
    }

    System.Collections.IEnumerator HandleSuccess()
    {
        if (objectToHide != null)
            objectToHide.SetActive(false);

        if (objectToShow != null)
            objectToShow.SetActive(true);

        Debug.Log("⏳ 等待 " + delayBeforeSceneLoad + " 秒后加载场景：" + nextSceneName);
        yield return new WaitForSeconds(delayBeforeSceneLoad);

        SceneManager.LoadScene(nextSceneName);
    }
}
