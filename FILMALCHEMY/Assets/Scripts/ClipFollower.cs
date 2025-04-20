using UnityEngine;

public class ClipFollower : MonoBehaviour
{
    [Header("��Ҫ�������Ƭ")]
    public Transform[] photos;

    [Header("��Ƭ��Լ��ӵ�ƫ��")]
    public Vector3[] offsets;

    void LateUpdate()
    {
        if (photos == null || offsets == null) return;

        for (int i = 0; i < photos.Length; i++)
        {
            if (photos[i] != null && i < offsets.Length)
            {
                photos[i].position = transform.position + offsets[i];
            }
        }
    }
}