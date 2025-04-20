using UnityEngine;

public class OriginalTransformRecorder : MonoBehaviour
{
    public Vector3 OriginalPosition { get; private set; }
    public Quaternion OriginalRotation { get; private set; }

    void Awake()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
    }

    public void StoreNow()
    {
        OriginalPosition = transform.position;
        OriginalRotation = transform.rotation;
    }
}