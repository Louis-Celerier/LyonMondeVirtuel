using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    [SerializeField] private List<Vector3> _testRoadVertices;
    private NetworkDisplayer _displayer;
    public List<Road> roads;
    [SerializeField] private int _roadsCount;

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
        for(int i = 0; i < _roadsCount; i++)
        {
            roads.Add(new Road(_testRoadVertices, 2, 4));
        }
        Debug.Log("Nodes created : " + roads.Count);
    }

    public void Refresh()
    {
        _displayer.DisplayNetwork();
    }
}
