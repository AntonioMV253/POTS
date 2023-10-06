using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
    public static float AngleTowardsTouch(Vector3 pos, Vector3 touchPos)
    {
        touchPos.z = pos.z;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(pos);
        touchPos.x = touchPos.x - objectPos.x;
        touchPos.y = touchPos.y - objectPos.y;

        float angle = Mathf.Atan2(touchPos.y, touchPos.x) * Mathf.Rad2Deg;

        return angle;
    }
}
