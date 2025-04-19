using UnityEngine;

public class ClickAreaSpawner : MonoBehaviour
{
    [Header("阶段一提示")]
    public GameObject objectToShow;
    public float showDuration = 3f;

    [Header("显影控制器")]
    public PhotoDevelopController photoDevelopController;  // 拖入你的显影控制器对象

    private Camera mainCamera;
    private bool hasClicked = false;
    private int step = 0;

    void Start()
    {
        mainCamera = Camera.main;

        if (objectToShow != null)
            objectToShow.SetActive(false);
    }

    void Update()
    {
        // Step 0：点击后显示提示物体
        if (Input.GetMouseButtonDown(0) && !hasClicked && step == 0)
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

        // 其他步骤逻辑可以交由其他控制器处理
    }

    System.Collections.IEnumerator ShowAndNextStep()
    {
        objectToShow.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        objectToShow.SetActive(false);
        step = 1;
        Debug.Log("进入 Step 1");
    }

    public void TriggerRotation(Transform target, float angle, float duration, int nextStep)
    {
        StartCoroutine(RotateAndNext(target, angle, duration, nextStep));
    }

    System.Collections.IEnumerator RotateAndNext(Transform target, float angle, float duration, int nextStep)
    {
        Quaternion startRot = target.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, angle);
        float elapsed = 0f;

        while (elapsed < duration)
        {
            target.rotation = Quaternion.Lerp(startRot, endRot, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.rotation = endRot;

        var drag = target.GetComponent<Draggable>();
        if (drag != null)
        {
            Destroy(drag);
            Debug.Log("已删除拖拽脚本");
        }

        step = nextStep;
        Debug.Log("进入 Step " + step);

        // ✅ 可在旋转后直接判断是否需要启动显影
        TryStartDevelopmentAtStep(step);
    }

    public void TriggerNextStep(int nextStep)
    {
        Debug.Log("手动跳转 Step " + step + " → " + nextStep);
        step = nextStep;

        // ✅ 当手动跳 step 时也判断是否要触发显影
        TryStartDevelopmentAtStep(step);
    }

    private void TryStartDevelopmentAtStep(int currentStep)
    {
        // 你可以更改这里的数字，比如只有 Step 4 才触发
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