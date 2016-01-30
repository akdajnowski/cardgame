using UnityEngine;

public class SwirlBehaviour : MonoBehaviour
{
    public float rotationSpeed = 1;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed, Space.World);
    }
}
