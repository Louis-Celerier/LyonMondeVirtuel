using GSD.Roads;
using System.Collections.Generic;
using UnityEngine;

public class Road
{
    private List<Vector3> _nodes;
    private int _realWidth;
    private int _pluginWidth;
    private int _pluginLanesCount;

    public Road(List<Vector3> nodes, int pluginWidth, int pluginLanesCount)
    {
        _nodes = nodes;
        _pluginWidth = pluginWidth;
        _pluginLanesCount = pluginLanesCount;
    }

    // Affichage de la route avec Road Architect
    public GSDRoad BuildRoad(ref GSDRoadSystem _roadSystem, bool showGizmo = false)
    {
        // Affichage de la route à l'aide de la classe GSDRoadAutomation
        var road = GSDRoadAutomation.CreateRoad_Programmatically(_roadSystem, ref _nodes);

        // Affichage/dissimulation du gizmo
        road.opt_GizmosEnabled = showGizmo;

        // Remove terrain modification
        road.opt_HeightModEnabled = false;
        road.opt_TreeModEnabled = false;
        road.opt_DetailModEnabled = false;
        return road;
    }

    public void ConfigureRoadAppearance(in GSDRoad road, int lanesCount = 2, GSDRoad.RoadMaterialDropdownEnum material = GSDRoad.RoadMaterialDropdownEnum.Asphalt, float shoulderWidth = 0)
    {
        road.opt_tRoadMaterialDropdown = material;
        //road.opt_Lanes = lanesCount; // nombre de voies, pouvant valoir 2, 4 ou 6
        road.opt_Lanes = _pluginLanesCount; // nombre de voies, pouvant valoir 2, 4 ou 6
        road.opt_LaneWidth = _pluginWidth; // largeur des voies
        road.opt_ShoulderWidth = shoulderWidth; // largeur des bandes d'arrêt d'urgence
    }
}
