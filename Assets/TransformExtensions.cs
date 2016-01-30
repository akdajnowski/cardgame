using UnityEngine;
using DG.Tweening;

public static class TransformExtensions
{
    public static Transform RotateInPlace (this Transform transform, Vector3 target, float constAngle = 0)
    {
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        float deltaX = target.x - objectPos.x;
        float deltaY = target.y - objectPos.y;
        float angle = Mathf.Atan2 (deltaY, deltaX) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle + constAngle));

        return transform;
    }


    public static Transform MoveToPlace (this Transform transform, Vector3 target)
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint (target);
        pz.z = 0;
        transform.DOMove (pz, 2);
        return transform;
    }
}

