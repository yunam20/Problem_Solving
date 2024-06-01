using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrustumPlanes
{
    private readonly Plane[] planes;

    public FrustumPlanes(Camera camera)
    {
        planes = GeometryUtility.CalculateFrustumPlanes(camera);
    }

    public bool IsInsideFrustum(Bounds bounds)
    {
        Vector3[] corners = new Vector3[8];

        // Compute all 8 corners of the bounds
        corners[0] = bounds.min;
        corners[1] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        corners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        corners[3] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        corners[4] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        corners[5] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        corners[6] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        corners[7] = bounds.max;

        // Check each corner to see if any of them is inside the frustum
        foreach (Vector3 corner in corners)
        {
            bool inside = true;
            foreach (Plane plane in planes)
            {
                if (plane.GetDistanceToPoint(corner) < 0)
                {
                    inside = false;
                    break;
                }
            }
            if (inside)
            {
                return true;
            }
        }
        return false;
    }
}