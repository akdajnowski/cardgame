using UnityEngine;
using System.Collections;
using DG.Tweening;
using System.Linq;

public class MouseNavigation : MonoBehaviour
{
    public Rigidbody2D rigid;
    public GameObject marker;
    public bool onRoute;
    public float Force = 100;
    private Quaternion targetRotation;
    private Vector3 forceDirection;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10;
        var angle = Angle(mouse);
        targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.parent.rotation = targetRotation;
        if (Input.GetMouseButtonDown(0))
        {
            rigid.AddForce((mouse - transform.position) * Force);
        }

        if (Input.GetMouseButtonDown(1))
        {
            var current = transform.parent;
            transform.SetParent(transform.parent.parent);
            current.position = transform.position;
            transform.SetParent(current);
            transform.localPosition = Vector3.zero;
        }

    }


    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
    {
        Vector3 dir = point - pivot; // get point direction relative to pivot
        dir = Quaternion.Euler(angles) * dir; // rotate it
        point = dir + pivot; // calculate rotated point
        return point; // return it
    }

    private void P()
    {
        var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (marker != null) DestroyImmediate(marker);

            marker = new GameObject("marker");
            marker.AddComponent<BoxCollider2D>();
            marker.transform.position = new Vector3(mouse.x, mouse.y, 0);


            var angle = Angle(marker.transform.position);

            targetRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            var eulerAngles = targetRotation.eulerAngles;
            var l = eulerAngles.z < 360 && eulerAngles.z > 180;


            forceDirection = (marker.transform.position - transform.position).normalized;
            rigid.AddForce(forceDirection * Force);
            onRoute = true;
        }

        if (onRoute)
        {
            rigid.AddForce(forceDirection * Force);
        }
        else
        {
            rigid.velocity = Vector2.zero;
        }

        if (targetRotation != null && !new float[] { targetRotation.x, targetRotation.z, targetRotation.y, targetRotation.w }.All(f => f == 0f))
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 10);
    }

    public void ApplyPosTorque()
    {
        rigid.AddTorque(50);
    }

    public void ApplyNegTorque()
    {
        rigid.AddTorque(-50);
    }

    private static float Angle(Vector3 pos)
    {
        return Mathf.Atan2(pos.y, pos.x) * Mathf.Rad2Deg;
    }

    public static float Angle(Vector2 p_vector2)
    {
        if (p_vector2.x < 0)
        {
            return 360 - (Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg * -1);
        }
        else
        {
            return Mathf.Atan2(p_vector2.x, p_vector2.y) * Mathf.Rad2Deg;
        }
    }


    void OnDrawGizmosSelected()
    {
        if (marker != null)
        {
            var mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(marker.transform.position, transform.position);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, mouse);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(coll.gameObject);
        onRoute = false;
    }
}