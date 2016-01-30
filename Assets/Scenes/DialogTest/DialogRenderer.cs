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
    public DialogEngine dialogEngine;

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

        Func<int> counter = FP.Inc();

        var dialog = dialogs.Dialogs.First();
        Render(dialog);
    }

    private void Render(Dialog dialog)
    {
        var dialogPanel = Instantiate(DialogPanel);
        dialogPanel.transform.SetParent(target.transform);
        dialogPanel.localPosition = Vector3.zero;
        var textPanelBehaviour = dialogPanel.GetComponent<TextPanelBehaviour>();
        textPanelBehaviour.SelectionCallback(HandleOption(dialog));
        textPanelBehaviour.SetDialog(dialog);
    }

    private Action<DialogOption> HandleOption(Dialog dialog)
    {
        return opt => dialogEngine.Handle(dialog, opt);
    }

    private static Vector3 GetHeightPointer(Func<int> func, TextPanelBehaviour tpb)
    {
        return Vector3.up * func() * ((RectTransform)tpb.transform).rect.height;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
