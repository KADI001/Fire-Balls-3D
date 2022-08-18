using UnityEngine;

public static class TransformExtension
{
    public static Vector3 GetPositionWithNewY(this Transform transform, float y)
    {
        Vector3 newPosition = transform.position;
        newPosition.y = y;
        return newPosition;
    }
}
