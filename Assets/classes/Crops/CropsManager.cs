using System.Collections.Generic;
using UnityEngine;

public static class CropManager
{
    private static List<Crop> crops = new List<Crop>();

    public static void RegisterCrop(Crop c)
    {
        crops.Add(c);
    }

    public static void UnregisterCrop(Crop c)
    {
        crops.Remove(c);
        Object.Destroy(c.gameObject);
    }

    public static Crop GetClosestCrop(Vector3 pos)
    {
        Crop closest = null;
        float minDist = Mathf.Infinity;

        foreach (var c in crops)
        {
            float dist = Vector3.Distance(pos, c.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = c;
            }
        }

        return closest;
    }

    public static void DestroyCrop(Crop c)
    {
        UnregisterCrop(c);
    }
}
