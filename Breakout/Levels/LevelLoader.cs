namespace Breakout.Levels;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Breakout.IO;


public class LevelLoader {
    private string[] legend;
    private string[] meta;
    private string[] map;

    public LevelLoader(string fileName) {
        string filePath = Path.Combine(PathFinder.Levels(), fileName);
        try {
            map = File.ReadLines(filePath)
               .SkipWhile(map => map != "Map:")
               .Skip(1) // Skip the intro line
               .TakeWhile(map => map != "Map/")
               .ToArray();

            meta = File.ReadLines(filePath)
                .SkipWhile(meta => meta != "Meta:")
                .Skip(1) // Skip the intro line
                .TakeWhile(meta => meta != "Meta/")
                .ToArray();
            legend = File.ReadLines(filePath)
                .SkipWhile(legend => legend != "Legend:")
                .Skip(1) // Skip the intro line
                .TakeWhile(legend => legend != "Legend/")
                .ToArray();
        } catch (System.IO.FileNotFoundException) {
        //If file does not exist, it will create a default map
           map = new List<string>{"hhhhhhhhhhh"}.ToArray();
           meta = new List<string>{":"}.ToArray();
           legend = new List<string>{"h) green-block.png"}.ToArray();
        }
    }

    public string[] GetMap() {
        List<string> mapLowerCase = new List<string> { };
        foreach (string m in map) {
            mapLowerCase.Add(m.ToLower());
        }
        return mapLowerCase.ToArray();
    }

    private Dictionary<string, string> GetDict(string[] data, string separator) {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        foreach (string line in data) {
            string[] parts = line.Split(separator);
            string key = parts[0].Trim().ToLower();
            string value = parts[1].Trim().ToLower();
            dict.Add(key, value);
        }
        return dict;
    }

    public Dictionary<string, string> GetMetaDict() {
        return GetDict(this.meta, ":");
    }

    public Dictionary<string, string> GetLegendDict() {
        return GetDict(this.legend, ")");
    }

    public bool MetaContains(string key, string value) {
        var metaDict = GetMetaDict();
        return (metaDict.ContainsKey(key) ?
                metaDict[key].Contains(value) : false);
    }
}