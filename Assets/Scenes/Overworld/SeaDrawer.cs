using System.Collections;
using UnityEngine;

public class SeaDrawer : MonoBehaviour
{
    public GameObject wave;
    public GameObject emitter;
    public GameObject target;
    public bool emitterIsTarget;

    [Range(0, 10)]
    public float SpawnEvery = 2;

    [Range(0, 100)]
    public int Distance = 10;

    [Range(-100, 100)]
    public float RepelForce = 10;

    void Start()
    {
        StartCoroutine(Trigger());
    }

    IEnumerator Trigger()
    {
        var rp = new Vector2(emitter.transform.position.x, emitter.transform.position.y) + Random.insideUnitCircle * Distance;
        var obj = Instantiate(wave);
        obj.GetComponent<WaveBehaviour>().detractor = (emitterIsTarget || target == null) ? emitter.transform : target.transform;
        obj.GetComponent<WaveBehaviour>().repelForce = RepelForce;
        obj.transform.SetParent(transform);
        obj.transform.position = rp;
        yield return new WaitForSeconds(SpawnEvery);
        StartCoroutine(Trigger());
    }
}
