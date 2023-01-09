using System;
using GeoJSON;
using UnityEngine;

public class RoadGeoJsonDisplayer : MonoBehaviour
{
    public RoadJsonParser roadJsonParser;
    public int DEFAULT_WIDTH = 2;
    public int DEFAULT_NUM_LANE = 2;
    
    public DebugRoad debugRoad;
    
    private FeatureCollection collection;

    private string KEY_WIDTH = "LARGEUR", KEY_NUM_LANE = "NB_VOIES";
    
    private void Awake() => collection = roadJsonParser.ReadRoad();

    private void Start()
    {
        foreach (var feature in collection.features)
        {
            Debug.Log("Width : " + GetWidth(feature) + " - Num Lane : " + GetNumLane(feature));
        }
        debugRoad.Draw(collection);
    }

    public int GetWidth(in FeatureObject feature)
    {
        if (feature.properties.ContainsKey(KEY_WIDTH))
        {
            string widthStr = feature.properties[KEY_WIDTH];
            return (int)Convert.ToDouble(widthStr);
        }

        return DEFAULT_WIDTH;
    }

    public int GetNumLane(in FeatureObject feature)
    {
        if (feature.properties.ContainsKey(KEY_NUM_LANE))
        {
            string laneStr = feature.properties[KEY_NUM_LANE];
            int numLane = (int)Convert.ToDouble(laneStr);
                
            // Clamp num lane of road
            if (5 < numLane)
            {
                return 6;

            }
            else if (3 < numLane)
            {
                return 4;
            }
            else
            {
                return 2;
            }
        }

        return DEFAULT_NUM_LANE;
    }
}
