using UnityEngine;

// Class for waypoints to be direction of object
public class WayPoints : MonoBehaviour
{
    public static Transform[] points;

    // Assign child waypoints when instance is called
    public void AddWayPoints()
    {
        points = new Transform[transform.childCount];

        // Insert location of waypoint's children
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
