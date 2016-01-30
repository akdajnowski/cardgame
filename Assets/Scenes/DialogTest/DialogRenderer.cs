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
    public Transform DialogPanel;
    public Transform target;
    private DialogsRoot dialogs;


    // Use this for initialization
    void Start()
    {
        this.Inject();
        var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
        var input = new StringReader(dialogAsset.text);
        dialogs = deserializer.Deserialize<DialogsRoot>(input);

        string currentIsland = Store.OverworldState.CurrentIsland.ToLower();


        var dialog = dialogs.Dialogs.FirstOrDefault(x => x.Id == currentIsland);
        if (dialog == null)
            throw new DialogMissingException(currentIsland);
        Render(dialog);
    }

    private void Render(Dialog dialog)
    {
        var dialogPanel = Instantiate(DialogPanel);
        dialogPanel.transform.SetParent(target.transform);
        dialogPanel.localPosition = Vector3.zero;
        var textPanelBehaviour = dialogPanel.GetComponent<TextPanelBehaviour>();
        textPanelBehaviour.SelectionCallback(() => Store.AdvanceState(Scenes.CardBattle));
        textPanelBehaviour.SetDialog(dialog);
    }

    public class DialogMissingException : Exception
    {
        public DialogMissingException(string dialogKey) : base("Missing dialog for " + dialogKey)
        {

        }
    }
}
