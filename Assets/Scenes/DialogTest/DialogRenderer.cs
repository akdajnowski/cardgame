using UnityEngine;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;
using System.IO;
using System.Linq;
using System;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using Adic;

public class DialogRenderer : MonoBehaviour
{

    [Inject]
    public GameStateStore Store { get; set; }

    public TextAsset dialogAsset;
    private DialogsRoot dialogs;
    private Text text;
    private CanvasGroup cg;
    private Button button;
    private Text buttonText;

    // Use this for initialization
    void Start ()
    {
        this.Inject ();
        Debug.Log ("Dialog Renderer behaviour injected");
        var deserializer = new Deserializer (namingConvention: new CamelCaseNamingConvention ());
        var input = new StringReader (dialogAsset.text);
        dialogs = deserializer.Deserialize<DialogsRoot> (input);
        var panel = transform.GetChild (0).GetChild (0).GetChild (0);
        button = transform.GetChild (0).GetChild (0).GetChild (1).GetComponent<Button> ();
        buttonText = button.transform.GetChild (0).GetComponent<Text> ();
        text = panel.GetComponent<Text> ();
        cg = transform.GetChild (0).GetChild (0).GetComponent<CanvasGroup> ();
        Alpha = 0;
    }

    public float Alpha {
        get { return cg.alpha; }
        set { cg.alpha = value; }
    }

    public void RunDialog (string currentIsland)
    {
        var dialog = dialogs.Dialogs.FirstOrDefault (x => x.Id == currentIsland);
        if (dialog == null)
            throw new DialogMissingException (currentIsland);
        Render (dialog);
    }

    private void Render (Dialog dialog)
    {
        DOTween.To(() => Alpha, x => Alpha = x, 1, 1f);

        button.onClick.RemoveAllListeners ();
        if (dialog.ToBattle ?? true) {
            buttonText.text = "Fight";
            button.onClick.AddListener (() => Store.AdvanceState (Scenes.CardBattle));
        } else {
            buttonText.text = "Continue";
            button.onClick.AddListener (() => DOTween.To (() => Alpha, x => Alpha = x, 0, 1f));
        }

        text.text = dialog.Description;
    }

    public class DialogMissingException : Exception
    {
        public DialogMissingException (string dialogKey) : base ("Missing dialog for " + dialogKey)
        {

        }
    }
}
