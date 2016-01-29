using UnityEngine;
using UnityEngine.UI;

public class WinningIndicator : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private Text text;

    // Use this for initialization
    void Start ()
    {
        canvasGroup = GetComponent<CanvasGroup> ();
        text = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update ()
    {

    }

    public void ShowWinner (string winner, Color color)
    {
        text.text = winner + " won";
        text.color = color;
        canvasGroup.alpha = 1;
    }
}
