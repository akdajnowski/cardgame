using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

public class YamlTest : MonoBehaviour {
	void Start () {
        //@"D:\Dev\GitHub\cardgame\Assets\Scenes\CardBattle"
        
        
        TextAsset asset = Resources.Load<TextAsset>("cardLibrary");
        Debug.Log("asset = " + asset.text);


        var input = new StringReader(cardInfo);
        var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
        //var card = deserializer.Deserialize<CardDescriptor>(input);
        //var file = System.IO.File.ReadAllText(@"D:\Dev\GitHub\cardgame\Assets\Scenes\CardBattle\cardLibrary.yml");
        //var stringx = new StringReader(text.text);
        //List<CardDescriptor> cards = deserializer.Deserialize<List<CardDescriptor>>(stringx);

        List<CardDescriptor> cards = new List<CardDescriptor>();

        using (var reader = File.OpenText(@"D:\Dev\GitHub\cardgame\Assets\Resources\cardLibrary.yml"))
        {
            cards = deserializer.Deserialize<List<CardDescriptor>>(reader);
        }

        foreach (var card in cards)
        {
            Debug.Log("name = " + card.Name);
            Debug.Log("image = " + card.Name);
            Debug.Log("attr = " + card.CardAttributes[0].Quality.ToString() + card.CardAttributes[0].Quantity.ToString());
            Debug.Log("attr = " + card.CardAttributes[1].Quality.ToString() + card.CardAttributes[1].Quantity.ToString());
            Debug.Log("attr = " + card.CardAttributes[2].Quality.ToString() + card.CardAttributes[2].Quantity.ToString());
            Debug.Log("attr = " + card.CardAttributes[3].Quality.ToString() + card.CardAttributes[3].Quantity.ToString());
            Debug.Log("attr = " + card.CardAttributes[4].Quality.ToString() + card.CardAttributes[4].Quantity.ToString());
        }
	}

	void Update () {
	
	}

    private string cardInfo = 
@"";
}
