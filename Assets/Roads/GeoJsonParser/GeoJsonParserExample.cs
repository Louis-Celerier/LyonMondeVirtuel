using System.IO;
using UnityEngine;
using Defective.JSON;
using GeoJSON;

public class GeoJsonParserExample : MonoBehaviour
{
    public string path ="C:\\Users\\wpetit\\Documents\\M2\\Monde_Virtuel\\tangram-unity\\Assets\\Custom\\";
    public string file = "road.geojson";

    private void Start()
    {
        var rawJson = File.ReadAllText(path + file);
        var jsonObject = new JSONObject(rawJson);
        
        Debug.LogError(rawJson);   
        //Read a TextAsset and parse as a FeatureCollection
        FeatureCollection collection = GeoJSON.GeoJSONObject.Deserialize(rawJson);
        Debug.Log(collection.features.Count);
    }
}
