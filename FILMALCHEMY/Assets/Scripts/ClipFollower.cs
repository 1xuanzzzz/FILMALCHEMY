using UnityEngine;

public class ClipFollower : MonoBehaviour
{
    [Header("需要跟随的照片")]
    public Transform[] photos;

    [Header("照片相对夹子的偏移")]
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