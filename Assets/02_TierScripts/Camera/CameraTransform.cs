using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraTransform : MonoBehaviour
{
    public Transform Target;
    public Vector3 Offset;

    private void LateUpdate()
    {
        transform.position = Target.position + Offset;
    }
}
