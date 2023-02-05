using GeoJSON;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    private NetworkDisplayer _displayer;
    public List<Road> roads;

    [Header("JSON")]
    [SerializeField] private RoadJsonParser _roadJsonParser;
    [SerializeField] private RoadGeoJsonDisplayer _roadGeoJsonDisplayer;
    private FeatureCollection _roadsCollection;
    [SerializeField] private Vector3 offset;

    [Header("Optimization settings")]
    [SerializeField] private int startRoadIndex = 0;                                        // Start of road creation, ignore all roads before these index
    [SerializeField, Range(0,10)] private int minRoadVertices = 3;
    [SerializeField, Range(0, 1)] private float _displayedRoadPercentage;
    [SerializeField, Range(1, 10)] private int _loopingStepsCount;

    // Start is called before the first frame update
    void Start()
    {
        // Creation of the roads, with their data
        roads = new List<Road>();
        SetupNetwork();
        // Displaying the roads
        _displayer = GetComponent<NetworkDisplayer>();
        Refresh();
    }

    public void SetupNetwork()
    {
        Debug.Log("Creating the roads...");
        // Getting the data from JSON file
        _roadsCollection = _roadJsonParser.ReadRoad();
        
        // Using all roads' data for setting them up
        int maxDisplayRoad = startRoadIndex + (int)(_roadsCollection.features.Count * _displayedRoadPercentage);
        Debug.LogError("Breaking : " + maxDisplayRoad);
        
        for (int i = startRoadIndex; i < maxDisplayRoad; i++)
        {
            var feature = _roadsCollection.features[i];
            List<PositionObject> allPositions = feature.geometry.AllPositions();
            
            int chosenLoopingStep = 1;
            
            // Afin d'�viter que des routes ne soient pas affich�es, on v�rifie le nombre de noeuds. Une route a besoin de 2 noeuds minimum pour �tre affich�e.
            if (allPositions.Count <= minRoadVertices)
            {
                Debug.LogWarning("Not enough vertices to display a road.");
                continue;
            }
            
            if (allPositions.Count >= 2 && allPositions.Count / chosenLoopingStep >= 2)
                chosenLoopingStep = _loopingStepsCount;
            
            List<Vector3> allNewNodes = new List<Vector3>();
            for (int j = 0; j < allPositions.Count; j = j + chosenLoopingStep)
            {
                Vector3 nodePosition = new Vector3(allPositions[j].latitude, 0f, allPositions[j].longitude) + offset;
                allNewNodes.Add(nodePosition);
            }

            var road = new Road(allNewNodes, _roadGeoJsonDisplayer.GetWidth(feature),
                _roadGeoJsonDisplayer.GetNumLane(feature));
            roads.Add(road);
        }
        Debug.Log("Roads done : " + roads.Count);
    }

    public void Refresh()
    {
        _displayer.DisplayNetwork();
    }
}
