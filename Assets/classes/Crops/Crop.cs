using UnityEngine;

public class Crop : MonoBehaviour
{
    public int cropID;

    void Start()
    {
        CropManager.RegisterCrop(this);
    }
}
