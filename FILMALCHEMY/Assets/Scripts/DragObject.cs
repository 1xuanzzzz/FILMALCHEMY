using UnityEngine;
using DG.Tweening;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 originalPosition; // 记录原始位置

    public Transform dropZone; // 目标区域（黑色背景）
    public Transform correctPosition; // 目标吸附位置
    public float radius;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position; // 记录初始位置
    }

    void OnMouseDown()
    {
        // 计算鼠标点击的偏移量
        offset = transform.position - GetMouseWorldPosition();
        isDragging = true;
        Debug.Log("On Mouse Down");
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPosition() + offset;
        }
        Debug.Log("On Mouse Drag");

    }

    void OnMouseUp()
    {
        isDragging = false;

        // 检测是否在目标区域内
        if (Vector3.Distance(transform.position, dropZone.position) < 1.5f) // 允许一定误差
        {
            // 吸附到正确位置
            transform.DOMove(correctPosition.position, 0.5f).SetEase(Ease.OutQuad);
        }
        else
        {
            // 返回原始位置
            transform.DOMove(originalPosition, 0.5f).SetEase(Ease.OutQuad);
        }
        Debug.Log("On Mouse Up");

    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z; // 保持物体的世界Z坐标
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
