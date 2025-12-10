using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/Enemy Data")]
public class EnemySummonData : ScriptableObject
{
    public int ID;
    public GameObject prefab;

    public float maxHealth = 100f;
    public float speed = 2f;
}
