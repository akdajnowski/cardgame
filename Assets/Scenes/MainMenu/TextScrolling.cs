using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextScrolling : MonoBehaviour
{
    private Text credits;
    public int ScrollSpeed;

    void Start ()
    {
        credits = gameObject.GetComponent<Text> ();
    }

    void Update ()
    {
        credits.transform.Translate (new Vector3 (0, ScrollSpeed, 0));
    }
}
