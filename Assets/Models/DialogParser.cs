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
          - id: 1
            description: |
              The great volcano eruption tears the island apart as soon as you approach it. You stare at rivers of lava and through the shroud of thick smoke. You could swear that lava starts to pile up and take a human-shaped form.
              ""It's a lava giant!"" the second mate cries.
              The lava pile is over 15 foot tall now.Inside the head-shaped bubble on the top the mouth like hole opens and deafening roar tears the air.The monster starts approaching your ship.There's little time to make decisions""
            options:
              - label: ""Try to run""
                outcome:
                  type: ""flee""
                  chance: 0.5
                  loss:
                    hp: [0, 7]
                    crew: [2, 5]
                    comment: ""The lava giant is faster than expected he reaches you in no time and starts attacking!""
              - label: ""Pick up a fight""
                outcome:
                  type: ""fight""
                  gain:
                    crew: [4, 7]
                    comment: ""The giant turns into rock. You won!""
              - label: ""Try to reason with him""
                outcome:
                  type: ""quest""
                  chance: 0.1
                  gain:
                    hp: [0, 0]
                    crew: [1, 3]
                    comment: ""The giant unexpectadly turns around and walks away. You have no idea why. When you prepare to leave, it appears there are some men on the island who wants to join your crew""
                  loss:
                    hp: [10, 10]
                    crew: [2, 5]
                    comment: ""The giant is not something you can reason win, he hits your ship with his lava fist giving considerable damage. You barely escape loosing some of your crew""

          - id: 2
            description: |
              The island seems to be deserted. You send the expedition but nobody returns.What do you do?
            options:
               - label: ""Send another expedition""
                 outcome:
                 type: ""quest""
                 chance: 0.3
                 loss:
                   hp: [0, 0]
                   crew: [2, 5]
                   comment: ""Your men found a friendly village and spent some good time at a party. They made some new friends who wish to join your journey""
                 gain:
                   hp: [0, 0]
                   crew: [2, 7]
	             - label: ""Leave them to their fate""
                 outcome:
                 type: ""flee""
                 loss:
                   hp: [0, 0]
                   crew: [2, 5]
                   comment: ""Your order to set sails and leave the island leaving your men to unknown fate""";
}
