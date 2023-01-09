using UnityEngine;
using GeoJSON;
using System.IO;

public class RoadJsonParser : MonoBehaviour
{
    public string folderPath = "\\Roads\\Resources\\";
    public string file = "road.geojson";
    
    public FeatureCollection ReadRoad()
    {
        var path = Application.dataPath + folderPath;
        var content = File.ReadAllText(path + file);
        FeatureCollection collection = GeoJSON.GeoJSONObject.Deserialize(content);
        return collection;
    }
}
