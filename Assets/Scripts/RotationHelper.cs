using UnityEngine;
using System.Collections;


public static class RotationHelper
{
    public static float GetAngleFromQuaternion(Quaternion rotation)
    {
        var angle = (rotation.eulerAngles.z + 360) % 360;
        return angle;
    }

    public static float GetAngleFromToTarget(Vector2 source, Vector2 target)
    {
        var vectorToTarget = (target - source).normalized;
        Quaternion rotationToTarget = Quaternion.FromToRotation(Vector3.up, vectorToTarget);
        var angleToTarget = (rotationToTarget.eulerAngles.z + 360) % 360;
        return angleToTarget;
    }

    public static float GetDifferenceBetweenAngles(float source, float target)
    {
        if (Mathf.Abs(source - target) > 180f)
        {
            if (source < 180f)
            {
                source += 360f;
            }

            if (target < 180f)
            {
                target += 360f;
            }
        }

        return target - source;
    }
}
