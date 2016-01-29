using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    private float interpVelocity;
    private float minDistance = 0;
    private float followDistance = 0;
    public GameObject target;
    private Vector3 targetPos;

    void Start ()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        if (target) {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 3f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime); 

            transform.position = Vector3.Lerp (transform.position, targetPos, 0.25f);

        }
    }
}