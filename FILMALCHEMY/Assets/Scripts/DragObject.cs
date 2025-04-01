using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public class DragObject : MonoBehaviour
{
        private Camera mainCamera;
        private Vector3 offset;
        private Vector3 originalPosition;
        private bool isDragging = false;
        private bool isPlaced = false;
        private bool isInSnapZone = false;

    public Transform correctPosition;          
        public bool isCorrectObject = false;        

        void Start()
        {
            mainCamera = Camera.main;
            originalPosition = transform.position;
        }

        void OnMouseDown()
        {
            if (isPlaced) return;

            offset = transform.position - GetMouseWorldPosition();
            isDragging = true;
        }

        void OnMouseDrag()
        {
            if (isDragging && !isPlaced)
            {
                transform.position = GetMouseWorldPosition() + offset;
            }
        }

        void OnMouseUp()
        {
        if (isInSnapZone && isCorrectObject)
        {
            transform.DOMove(correctPosition.position, 0.5f).SetEase(Ease.OutQuad);
            isPlaced = true;
            GameManager.Instance.RegisterCorrectPlacement();
        }
        else
        {
            ReturnToOrigin();
        }
    }

        void ReturnToOrigin()
        {
        transform.DOShakePosition(0.3f, 0.3f, 10, 90, false, true)
            .OnComplete(() =>
         {
        transform.DOMove(originalPosition, 0.5f).SetEase(Ease.OutQuad);
          });
         }

        Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Mathf.Abs(mainCamera.transform.position.z); 
            return mainCamera.ScreenToWorldPoint(mousePosition);
        }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SnapZone"))
        {
            isInSnapZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("SnapZone"))
        {
            isInSnapZone = false;
        }
    }
}




