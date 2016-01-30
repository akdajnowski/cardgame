using UnityEngine;
using System.Collections;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.Collections.Generic;
using System.Text;

public class DialogParser : MonoBehaviour {

    // Use this for initialization
    void Start() {
        var input = new StringReader(Document);

        var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());

        var dialogs = deserializer.Deserialize<DialogsRoot>(input);

        var output = new StringBuilder();

        output.Append(dialogs.Dialogs[0].Description);
        foreach(var opt in dialogs.Dialogs[0].Options)
        {
            output.Append("label: " + opt.Label);
            output.Append("chance: "+opt.Outcome.Chance);

        }
        dialogs.Dialogs[0].Options.ForEach(x => { output.Append(x.Label); });
        

        Debug.Log(output.ToString());

    }

    private const string Document = @"---
        dialogs:
          - description: |
              The great volcano eruption tears the island apart as soon as you approach it. You stare at rivers of lava and through the shroud of thick smoke. You could swear that lava starts to pile up and take a human-shaped form. 
              ""It's a lava giant!"" the second mate cries. 
              The lava pile is over 15 foot tall now.Inside the head-shaped bubble on the top the mouth like hole opens and deafening roar tears the air.The monster starts approaching your ship.There's little time to make decisions
            options:
              - label: ""Try to run""
                outcome: 
                  type: ""flee""
                  chance: 0.5
                  loss:
                    hp: [3, 7]
                    crew: [2, 5]
              - label: ""Pick up a fight""
                outcome: 
                  type: ""fight""
                  gain:
                    crew: [4, 7]
              - label: ""Try to reason with him""
                outcome:
                  type: ""negotiations""
                  chance: 0.1
                  gain: 
                    crew: [1, 3]
                  loss:
                    hp: [10, 10]
                    crew: [2, 5]";
}
