using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkDisplayer : MonoBehaviour
{
    [SerializeField] private GSDRoadSystem _network;
    private List<GSDRoad> _roadsDisplays;
    private NetworkManager _manager;
    [SerializeField] private bool _enablingGizmo;

    // Start is called before the first frame update
    void Start()
    {
        _roadsDisplays = new List<GSDRoad>();
        _manager = GetComponent<NetworkManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Affichage de toutes les routes du réseau
    public void DisplayNetwork()
    {
        Debug.Log("Displaying roads");
        _network.opt_bMultithreading = false;
        _network.opt_bAllowRoadUpdates = false;
        foreach(var road in _manager.roads) 
        {
            Debug.Log("Displaying single road");
            GSDRoad roadDisplay = road.BuildRoad(ref _network, _enablingGizmo);
            road.ConfigureRoadAppearance(roadDisplay);
            _roadsDisplays.Add(roadDisplay);
        }
        _network.opt_bAllowRoadUpdates = true;
        _network.UpdateAllRoads();
    }
}
