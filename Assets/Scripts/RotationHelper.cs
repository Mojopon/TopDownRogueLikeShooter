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
        float angleRad = Mathf.Atan2(target.y - source.y, target.x - source.y);
        float angleDeg = (180 / Mathf.PI) * angleRad;

        // Atan2 calculates as X is forward so we need to subtract 90 degrees from the result to make Y axis to be forward 
        angleDeg -= 90;

        return angleDeg;
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
