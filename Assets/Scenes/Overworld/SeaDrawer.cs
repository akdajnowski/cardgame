using System.Collections;
using UnityEngine;

public class SeaDrawer : MonoBehaviour
{
    public GameObject wave;
    public GameObject ship;

    [Range(0, 10)]
    public int SpawnEvery = 2;

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
        var rp = new Vector2(ship.transform.position.x, ship.transform.position.y) + Random.insideUnitCircle * Distance;
        var obj = Instantiate(wave);
        obj.GetComponent<WaveBehaviour>().detractor = ship.transform;
        obj.GetComponent<WaveBehaviour>().repelForce = RepelForce;
        obj.transform.SetParent(transform);
        obj.transform.position = rp;
        yield return new WaitForSeconds(SpawnEvery);
        StartCoroutine(Trigger());
    }
}
