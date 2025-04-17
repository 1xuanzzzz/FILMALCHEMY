using UnityEngine;

public class ClickAreaSpawner : MonoBehaviour
{
    public GameObject objectToShow;
    public float showDuration = 3f;

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
    }

    System.Collections.IEnumerator ShowAndNextStep()
    {
        objectToShow.SetActive(true);
        yield return new WaitForSeconds(showDuration);
        objectToShow.SetActive(false);
        step = 1;
    }

    public int CurrentStep()
    {
        return step;
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
        Debug.Log("进入下一阶段 Step: " + step);
    }

    public void TriggerNextStep(int nextStep)
    {
        step = nextStep;
    }
}