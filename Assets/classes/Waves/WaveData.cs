using UnityEngine;

[System.Serializable]
public class WaveData
{
    public string name;

    public EnemyWaveEntry[] enemies;
}

[System.Serializable]
public class EnemyWaveEntry
{
    public int enemyID;
    public int count;
}
