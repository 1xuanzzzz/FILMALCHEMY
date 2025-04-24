using UnityEngine;

public class ClickAreaSpawner : MonoBehaviour
{
    [Header("阶段一提示")]
    public GameObject objectToShow;
    public float showDuration = 3f;

    [Header("显影控制器")]
    public PhotoDevelopController photoDevelopController;

    private Camera mainCamera;
    private bool hasClicked = false;
    public int step = 0;

    void Start()
    {
        mainCamera = Camera.main;

        if (objectToShow != null)
            objectToShow.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !hasClicked && step == 1)
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("点击到了区域！");
                hasClicked = true;
                StartCoroutine(ShowAndNextStep());
            }
        }
    }

    System.Collections.IEnumerator ShowAndNextStep()
    {
        objectToShow.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        objectToShow.SetActive(false);
        step = 2;
        Debug.Log("进入 Step 1");
    }

    public void TriggerRotation(Transform target, float angle, float duration, int nextStep)
    {
        StartCoroutine(RotateAndNext(target, angle, duration, nextStep));
    }

    System.Collections.IEnumerator RotateAndNext(Transform target, float angle, float duration, int nextStep)
    {
        OriginalTransformRecorder recorder = target.GetComponent<OriginalTransformRecorder>();
        if (recorder == null)
        {
            Debug.LogError("⚠️ 缺少 OriginalTransformRecorder 组件，无法还原 Transform");
            yield break;
        }

        Quaternion originalRotation = recorder.OriginalRotation;
        Vector3 originalPosition = recorder.OriginalPosition;

        Quaternion liftedRotation = originalRotation * Quaternion.Euler(0, 0, angle);

        // ✅ 当前拖后的 position，要保持不动
        Vector3 currentPosition = target.position;

        // ✅ Step 1：原地旋转动画，位置保持当前
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            target.rotation = Quaternion.Lerp(originalRotation, liftedRotation, t);
            target.position = currentPosition; // ✅ 不动位置
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.rotation = liftedRotation;
        target.position = currentPosition;

        // ✅ Step 2：保持当前状态 3 秒
        yield return new WaitForSeconds(2f);

        // ✅ Step 3：瞬间回到拖动之前的位置和角度
        target.rotation = originalRotation;
        target.position = originalPosition;

        var drag = target.GetComponent<Draggable>();
        if (drag != null)
        {
            Destroy(drag);
            Debug.Log("已删除拖拽脚本");
        }

        step = nextStep;
        Debug.Log("进入 Step " + step);
        TryStartDevelopmentAtStep(step);
    }

    public void TriggerNextStep(int nextStep)
    {
        Debug.Log("手动跳转 Step " + step + " → " + nextStep);
        step = nextStep;
        TryStartDevelopmentAtStep(step);
    }

    private void TryStartDevelopmentAtStep(int currentStep)
    {
        if (currentStep == 4 && photoDevelopController != null)
        {
            photoDevelopController.StartDevelopment();
            Debug.Log("▶️ 显影流程启动（来自 Step " + currentStep + "）");
        }
    }

    public int CurrentStep()
    {
        return step;
    }
}