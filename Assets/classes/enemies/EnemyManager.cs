using UnityEngine;
using System.Collections.Generic;

public static class EnemyManager
{
    private static Dictionary<int, EnemySummonData> enemyData;            // Loaded ScriptableObjects
    private static Dictionary<int, Queue<Enemy>> pools;                   // Object pools by ID
    private static List<Enemy> activeEnemies;                             // Enemies currently alive

    private static bool initialized = false;

    public static void Init()
    {
        if (initialized) return;

        enemyData = new Dictionary<int, EnemySummonData>();
        pools = new Dictionary<int, Queue<Enemy>>();
        activeEnemies = new List<Enemy>();

        // Load ALL EnemySummonData assets inside Resources/Enemies/
        EnemySummonData[] datas = Resources.LoadAll<EnemySummonData>("Enemies");



        foreach (var data in datas)
        {
            if (!enemyData.ContainsKey(data.ID))
            {
                enemyData[data.ID] = data;
                pools[data.ID] = new Queue<Enemy>();
            }
            else
            {
                Debug.LogWarning($"Duplicate Enemy ID found: {data.ID}. Only the first will be used.");
            }
        }

        initialized = true;
    }


    /// <summary>
    /// Spawns an enemy (from pool if possible).
    /// </summary>
    public static Enemy Summon(int ID, Vector3 position)
    {
        if (!enemyData.ContainsKey(ID))
        {
            Debug.LogError($"Enemy with ID {ID} not found in EnemySummonData!");
            return null;
        }

        EnemySummonData data = enemyData[ID];
        Enemy enemy;

        // Use from pool?
        if (pools[ID].Count > 0)
        {
            enemy = pools[ID].Dequeue();
            enemy.gameObject.SetActive(true);
            enemy.transform.position = position;
        }
        else
        {
            GameObject obj = Object.Instantiate(data.prefab, position, Quaternion.identity);
            enemy = obj.GetComponent<Enemy>();
        }

        // Initialize stats on spawn
        enemy.Init(data);

        activeEnemies.Add(enemy);
        return enemy;
    }


    /// <summary>
    /// Returns enemy to the pool and removes it from active list.
    /// </summary>
    public static void ReturnEnemy(Enemy enemy)
    {
        if (enemy == null) return;

        if (activeEnemies.Contains(enemy))
            activeEnemies.Remove(enemy);

        enemy.gameObject.SetActive(false);
        pools[enemy.ID].Enqueue(enemy);
    }


    /// <summary>
    /// True if no enemies are alive.
    /// </summary>
    public static bool AllEnemiesDead => activeEnemies.Count == 0;
}
