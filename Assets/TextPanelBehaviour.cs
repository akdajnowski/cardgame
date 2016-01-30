using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPanelBehaviour : MonoBehaviour
{
    public Guid guid;
    public Dialog Dialog { get; private set; }
    private Text text;
    private bool started;
    private List<Transform> buttons;
    private Action<DialogOption> _handleOption;

    // Use this for initialization
    void Start()
    {
        started = true;
        text = transform.GetChild(0).GetComponent<Text>();
        buttons = new List<Transform>();
        foreach (Transform button in transform.GetChild(1))
        {
            buttons.Add(button);
        }
        guid = Guid.NewGuid();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDialog(Dialog dialog)
    {
        if (!started) Start();
        Dialog = dialog;
        text.text = Dialog.Description;

        for (int i = 0; i < buttons.Count; i++)
        {
            try
            {
                var opt = Dialog.Options[i];
                buttons[i].GetComponent<CanvasGroup>().alpha = 1;
                buttons[i].GetChild(0).GetComponent<Text>().text = opt.Label;
                buttons[i].GetComponent<Button>().onClick.AddListener(delegate { _handleOption(opt); });
            }
            catch (ArgumentOutOfRangeException _)
            {
                buttons[i].GetComponent<CanvasGroup>().alpha = 0;
            }
        }
    }

    public void SelectionCallback(Action<DialogOption> handleOption)
    {
        _handleOption = handleOption;
    }
}
