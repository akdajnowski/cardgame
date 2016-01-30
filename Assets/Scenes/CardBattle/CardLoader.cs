using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using YamlDotNet.Serialization.NamingConventions;
using YamlDotNet.Serialization;

public class CardLoader
{
    public List<CardDescriptor> LoadCards (string yamlFilePath)
    {
        

        /*var deserializer = new Deserializer(namingConvention: new CamelCaseNamingConvention());
        var file = File.OpenText(yamlFilePath);
        List<CardDescriptor> cards = deserializer.Deserialize<List<CardDescriptor>>(file);
        file.Close();
        return cards;*/

        return new List<CardDescriptor> ();
    }
}
