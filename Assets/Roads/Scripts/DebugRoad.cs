using GeoJSON;
using UnityEngine;

public class DebugRoad : MonoBehaviour
{
    public GameObject original;
    public Vector3 offset;
    
    public void Draw(FeatureCollection roadCollection)
    {
        foreach (var feature in roadCollection.features)
        {
            foreach (var position in feature.geometry.AllPositions())
            {
                PlacePlaceholderAt(new Vector3(position.latitude, 0f, position.longitude) + offset);
            }
        }
    }
    
    private void PlacePlaceholderAt(Vector3 position)
    {
        var clone = Instantiate(original, position, Quaternion.identity, transform);
    }
}
