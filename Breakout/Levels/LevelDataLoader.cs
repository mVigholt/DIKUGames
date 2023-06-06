namespace Breakout.Levels;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Breakout.IO;

/// <summary>
/// Load data from an ascii file and seprate the data in different fields.
/// The data in each field are stored as different arrays and dictionarys
/// If file does not exist, a default map will be created
/// </summary>
public class LevelDataLoader {
    private string[] legend;
    private string[] meta;
    private string[] map;

    public LevelDataLoader(string fileName) {
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
            map = new List<string> { "hhhhhhhhhhh" }.ToArray();
            meta = new List<string> { ":" }.ToArray();
            legend = new List<string> { "h) green-block.png" }.ToArray();
        }
    }

    /// <summary>
    /// Store the map field into an array. Each item in the array is one
    /// row in the map.
    /// </summary>
    public string[] GetMap() {
        List<string> mapLowerCase = new List<string> { };
        foreach (string m in map) {
            mapLowerCase.Add(m.ToLower());
        }
        return mapLowerCase.ToArray();
    }


    /// <summary>
    /// A general method to create a dictionary by differents separators.
    /// </summary>
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

    /// <summary>
    /// Create a meta dictionary, with descriptive functions
    /// as the key and symbols as the value
    /// </summary>
    public Dictionary<string, string> GetMetaDict() {
        return GetDict(this.meta, ":");
    }

    public Dictionary<string, string> GetLegendDict() {
        return GetDict(this.legend, ")");
    }

    /// <summary>
    /// Check if one dictionary contains one certain key.
    /// </summary>
    public bool MetaContains(string key, string value) {
        var metaDict = GetMetaDict();
        return (metaDict.ContainsKey(key) ?
                metaDict[key].Contains(value) : false);
    }
}