using UnityEngine;
using System.Collections;

public class HauntedShipBehaviour : MonoBehaviour
{
    private Rigidbody2D rigid;
    public GameObject target;
    public float speed;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var w = transform.position - target.transform.position;
        rigid.AddForce(w.normalized * speed);
    }
}
