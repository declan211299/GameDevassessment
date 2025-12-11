using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class CropManager
{
    private static List<Crop> crops = new List<Crop>();

    public static void RegisterCrop(Crop c)
    {
        if (!crops.Contains(c))
            crops.Add(c);
    }

    public static void UnregisterCrop(Crop c)
    {
        if (crops.Contains(c))
            crops.Remove(c);

        Object.Destroy(c.gameObject);

        if (crops.Count == 0)
        {
            Debug.Log("ALL CROPS LOST — GAME OVER");
            SceneManager.LoadScene("start");
        }
    }

    public static Crop GetClosestCrop(Vector3 pos)
    {
        Crop closest = null;
        float minDist = Mathf.Infinity;

        foreach (var c in crops)
        {
            if (c == null) continue;

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
