using UnityEngine;
using TMPro;
using DG.Tweening;

public class PhotoDevelopController : MonoBehaviour
{
    [Header("显影设置")]
    public GameObject photo;                  // 最终显影后要显示的照片
    public float simulationTime = 60f;           // 显影时间（秒）
    public float realDevelopTime = 5f;
    public string startTimerText;

    [Header("UI显示")]
    public TextMeshProUGUI timerText;         // 正计时文本（00:00:00）

    [Header("流程控制")]
    public ClickAreaSpawner clickAreaSpawner;
    public FinalDragOutController dragOutController;  // 拖出控制器（显影完成后启动）

    private bool isDeveloping = false;
    private bool hasStarted = false;
    private float timer = 0f;

    void Start()
    {
        // 一开始隐藏照片
        if (photo != null)
            photo.SetActive(false);

        timer = 0f;

        if (timerText != null)
            timerText.text = startTimerText;
    }

    void Update()
    {
        if (!isDeveloping) return;

        timer += Time.deltaTime;
        UpdateTimerUI();

        if (timer >= realDevelopTime)
        {
            CompleteDevelopment();
            TriggerShakeAnimation();
            timerText.text = "00:00:00";
            isDeveloping = false;
        }
    }

    public void StartDevelopment()
    {
        if (hasStarted) return;

        hasStarted = true;
        isDeveloping = true;
        timer = 0f;

        // 不显示照片，保持隐藏，直到完成再显示
        UpdateTimerUI();
        Debug.Log("▶️ 显影计时开始，照片尚未显示");
    }

    void CompleteDevelopment()
    {
        Debug.Log("✅ 显影完成");

        // ✅ 显影完成 → 显示照片 GameObject
        if (photo != null)
            photo.SetActive(true);

        // ✅ 通知进入下一个 Step
        if (clickAreaSpawner != null)
        {
            clickAreaSpawner.TriggerNextStep(6); // 进入拖出阶段
        }

        // ✅ 启用拖拽阶段控制器
        if (dragOutController != null)
        {
            dragOutController.enabled = true;
        }
    }

    void UpdateTimerUI()
    {
        if (timerText != null)
        {
            // float clampedTime = Mathf.Min(timer, simulationTime);
            float timeLeft = (realDevelopTime - timer) * (simulationTime/realDevelopTime);
            int hours = Mathf.FloorToInt(timeLeft / 3600);
            int minutes = Mathf.FloorToInt((timeLeft % 3600) / 60);
            int seconds = Mathf.FloorToInt(timeLeft % 60);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
        }
    }


    void TriggerShakeAnimation()
    {
        float duration = 3.0f; // 震动持续时间（秒）
        float strengthX = 10f; // X轴震动强度（单位是UI像素）
        int vibrato = 20;      // 震动次数
        float randomness = 0f; // 设为0表示只沿一个方向震动（X轴）

        RectTransform rect = timerText.rectTransform;
        rect.DOKill(); // 取消之前的Tween，避免冲突

        rect.DOShakePosition(duration,
                             new Vector3(strengthX, 0f, 0f),
                             vibrato,
                             randomness,
                             snapping: false,
                             fadeOut: true)
            .SetEase(Ease.Linear);
    }
}