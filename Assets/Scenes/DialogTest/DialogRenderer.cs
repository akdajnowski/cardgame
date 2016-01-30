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
    private DialogsRoot dialogs;


    // Use this for initialization
    void Start()
    {
        this.Inject();
        var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
        var input = new StringReader(dialogAsset.text);
        dialogs = deserializer.Deserialize<DialogsRoot>(input);

        string currentIsland = Store.OverworldState.CurrentIsland;


        var dialog = dialogs.Dialogs.FirstOrDefault(x => x.Id == currentIsland);
        if (dialog == null)
            throw new DialogMissingException(currentIsland);
        Render(dialog);
    }

    private void Render(Dialog dialog)
    {
        var textPanelBehaviour = DialogPanel.GetComponent<TextPanelBehaviour>();
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
