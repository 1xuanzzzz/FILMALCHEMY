using UnityEngine;
using DG.Tweening;

public class DragObject : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging = false;
    private Vector3 originalPosition; // ��¼ԭʼλ��

    public Transform dropZone; // Ŀ�����򣨺�ɫ������
    public Transform correctPosition; // Ŀ������λ��
    public float radius;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = transform.position; // ��¼��ʼλ��
    }

    void OnMouseDown()
    {
        // �����������ƫ����
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

        // ����Ƿ���Ŀ��������
        if (Vector3.Distance(transform.position, dropZone.position) < 1.5f) // ����һ�����
        {
            // ��������ȷλ��
            transform.DOMove(correctPosition.position, 0.5f).SetEase(Ease.OutQuad);
        }
        else
        {
            // ����ԭʼλ��
            transform.DOMove(originalPosition, 0.5f).SetEase(Ease.OutQuad);
        }
        Debug.Log("On Mouse Up");

    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.WorldToScreenPoint(transform.position).z; // �������������Z����
        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}
