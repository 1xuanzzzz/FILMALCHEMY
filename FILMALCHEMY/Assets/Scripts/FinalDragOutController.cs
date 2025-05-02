using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalDragOutController : MonoBehaviour
{
    [Header("拖拽检测")]
    public Transform photoObject;              // 拖的照片
    public RectTransform dropTargetArea;       // 拖拽目标区域

    [Header("显示隐藏物体")]
    public GameObject objectToHide;            // 要隐藏的 GameObject（非 UI 也可以）
    public GameObject objectToShow;            // 拖对之后需要显示的 GameObject

    [Header("场景切换")]
    public string nextSceneName = "NextScene";
    public float delayBeforeSceneLoad = 5f;

    private bool hasDroppedSuccessfully = false;

    void Start()
    {
        enabled = false; // 初始禁用，由显影控制器启用
        if (objectToShow != null)
            objectToShow.SetActive(false); // 初始隐藏
    }

    void Update()
    {
        if (hasDroppedSuccessfully) return;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, photoObject.position);
        if (RectTransformUtility.RectangleContainsScreenPoint(dropTargetArea, screenPoint))
        {
            hasDroppedSuccessfully = true;
            Debug.Log("照片成功拖到指定区域！");
            StartCoroutine(HandleSuccess());
        }
    }

    public System.Collections.IEnumerator HandleSuccess()
    {
        // 隐藏旧物体
        if (objectToHide != null)
            objectToHide.SetActive(false);

        // 显示新物体
        if (objectToShow != null)
            objectToShow.SetActive(true);

        Debug.Log("5秒后跳转场景：" + nextSceneName);
        yield return new WaitForSeconds(delayBeforeSceneLoad);

        SceneManager.LoadScene(nextSceneName);
    }
}