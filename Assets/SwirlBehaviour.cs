using UnityEngine;
using System.Collections;

public class SwirlBehaviour : MonoBehaviour
{
    private bool visited;
    // Use this for initialization
    void Start ()
    {
	
    }
	
    // Update is called once per frame
    void Update ()
    {
	
    }

    void CollisionPerform (int stuff)
    {
        Debug.Log ("Already visited: " + visited);
        if (!visited) {
            visited = true;
            Debug.Log ("Napierdalamy w swirlu z paramsem: " + stuff);
        }
    }
}
