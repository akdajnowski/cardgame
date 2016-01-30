using UnityEngine;
using System.Collections;

public class CursorApplier : MonoBehaviour
{
    public Texture2D cursor;

    // Use this for initialization
    void Start()
    {
        cursor = Resources.Load<Texture2D>(@"cursor");
        Cursor.SetCursor(cursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
