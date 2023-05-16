namespace Breakout.Levels;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using Breakout.IO;


public class LoadFile {
    public IEnumerable<string> legends {
        get; private set;
    }
    public IEnumerable<string> meta {
        get; private set;
    }
    public IEnumerable<string> maps {
        get; private set;
    }
    public LoadFile(string fileName) {
        string filePath = Path.Combine(PathFinder.Levels(), fileName);
        legends = File.ReadLines(filePath)
            .SkipWhile(legend => legend != "Legend:")
            .Skip(1) // Skip the intro line
            .TakeWhile(legend => legend != "Legend/");
        meta = File.ReadLines(filePath)
            .SkipWhile(meta => meta != "Meta:")
            .Skip(1) // Skip the intro line
            .TakeWhile(meta => meta != "Meta/");
        maps = File.ReadLines(filePath)
           .SkipWhile(map => map != "Map:")
           .Skip(1) // Skip the intro line
           .TakeWhile(map => map != "Map/");
    }

    public Dictionary<string, string> CreateLegends() {
        Dictionary<string, string> legendsDict = new Dictionary<string, string> { };
        foreach (string l in this.legends) {
            string[] parts = l.Split(')');
            string symbol = parts[0].Trim();
            string imagePath = parts[1].Trim().ToLower();
            legendsDict.Add(symbol, imagePath);
        }
        return legendsDict;
    }

    public Dictionary<string, string> CreateMetadata() {
        Dictionary<string, string> metaDict = new Dictionary<string, string> { };
        foreach (string pair in meta) {
            string[] parts = pair.Split(":");
            string property = parts[0].Trim().ToLower();
            string symbol = parts[1].Trim();
            metaDict[symbol] = property;
        }
        return metaDict;
    }

    public string[] CreateMap(){
        string[] bricks = maps.ToArray();
        return bricks;
    }
}
