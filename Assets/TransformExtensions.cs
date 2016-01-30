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

    public static Tweener RotateAnimate (this Transform transform, Vector3 target, float constAngle = 0, float animationTime = 1)
    {
        Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        float deltaX = target.x - objectPos.x;
        float deltaY = target.y - objectPos.y;
        float angle = Mathf.Atan2 (deltaY, deltaX) * Mathf.Rad2Deg;

        return transform.DORotate (new Vector3 (0, 0, angle + constAngle), animationTime, RotateMode.Fast);
    }



    public static Tweener MoveToPlace (this Transform transform, Vector3 target, float animationTime = 2)
    {
        Vector3 pz = Camera.main.ScreenToWorldPoint (target);
        pz.z = 0;
        return transform.DOMove (pz, animationTime);
    }
}

