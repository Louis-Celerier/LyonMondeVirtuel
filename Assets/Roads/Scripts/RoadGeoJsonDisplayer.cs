using GeoJSON;
using UnityEngine;

public class RoadGeoJsonDisplayer : MonoBehaviour
{
    public RoadJsonParser roadJsonParser;
    public DebugRoad debugRoad;
    
    private FeatureCollection collection;
    
    private void Awake() => collection = roadJsonParser.ReadRoad();

    private void Start()
    {
        debugRoad.Draw(collection);
    }
}
