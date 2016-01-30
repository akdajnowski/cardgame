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
    private Transform button;
    private Action _startBattle;

    // Use this for initialization
    void Start()
    {
        started = true;
        text = transform.GetChild(0).GetComponent<Text>();
        button = transform.GetChild(1);
        guid = Guid.NewGuid();
    }
    

    public void SetDialog(Dialog dialog)
    {
        if (!started) Start();
        Dialog = dialog;
        text.text = Dialog.Description;
        button.GetComponent<CanvasGroup>().alpha = 1;
        button.GetChild(0).GetComponent<Text>().text = "Fight";
        button.GetComponent<Button>().onClick.AddListener(() => _startBattle());
        
    }

    public void SelectionCallback(Action startBattle)
    {
        _startBattle = startBattle;
    }
}
