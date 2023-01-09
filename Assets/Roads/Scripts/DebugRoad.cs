using UnityEngine;

public class DebugRoad : MonoBehaviour
{
    public GameObject original;

    private void PlacePlaceholderAt(Vector3 position)
    {
        var clone = Instantiate(original, position, Quaternion.identity, transform);
    }
}
