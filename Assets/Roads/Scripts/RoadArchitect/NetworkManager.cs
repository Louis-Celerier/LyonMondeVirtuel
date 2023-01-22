using GeoJSON;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> _testRoadVertices;
    private NetworkDisplayer _displayer;
    public List<Road> roads;
    [SerializeField] private int _roadsCount;

    [Header("JSON")]
    [SerializeField] private RoadJsonParser _roadJsonParser;
    [SerializeField] private RoadGeoJsonDisplayer _roadGeoJsonDisplayer;
    private FeatureCollection _roadsCollection;
    [SerializeField] private Vector3 offset;

    [Header("Optimization settings")]
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetupNetwork()
    {
        Debug.Log("Creating the roads...");
        // Getting the data from JSON file
        _roadsCollection = _roadJsonParser.ReadRoad();
        //for(int i = 0; i < _roadsCount; i++)
        //{
        //    roads.Add(new Road(_testRoadVertices, 2, 4));
        //}
        // Using all roads' data for setting them up
        int breaking = 0;
        foreach (var feature in _roadsCollection.features)
        {
            if (breaking >= _roadsCollection.features.Count * _displayedRoadPercentage) 
            {
                break;
            }
            List<Vector3> allNewNodes = new List<Vector3>();
            //foreach (var position in feature.geometry.AllPositions())
            //{
            //    Vector3 nodePosition = new Vector3(position.latitude, 0f, position.longitude) + offset;
            //    allNewNodes.Add(nodePosition);
            //}
            List<PositionObject> allPositions = feature.geometry.AllPositions();
            int chosenLoopingStep = 1;
            // Afin d'éviter que des routes ne soient pas affichées, on vérifie le nombre de noeuds. Une route a besoin de 2 noeuds minimum pour être affichée.
            if (allPositions.Count >= 2 && allPositions.Count / chosenLoopingStep >= 2)
                chosenLoopingStep = _loopingStepsCount;
            for (int i = 0; i < allPositions.Count; i = i + chosenLoopingStep)
            {
                Vector3 nodePosition = new Vector3(allPositions[i].latitude, 0f, allPositions[i].longitude) + offset;
                allNewNodes.Add(nodePosition);
            }
            roads.Add(new Road(allNewNodes, _roadGeoJsonDisplayer.GetWidth(feature), _roadGeoJsonDisplayer.GetNumLane(feature)));
            breaking++;
        }
        Debug.Log("Roads done : " + roads.Count);
    }

    public void Refresh()
    {
        _displayer.DisplayNetwork();
    }
}
