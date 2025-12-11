using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public WaveData[] waves;      // You will set this to 6 waves in Inspector
    public Transform[] spawnPoints;

    private int currentWave = 0;

    void Start()
    {
        EnemyManager.Init();
        StartCoroutine(HandleWaves());
    }

    IEnumerator HandleWaves()
    {
        while (currentWave < waves.Length)
        {
            Debug.Log("Starting Wave " + (currentWave + 1));
            yield return StartCoroutine(SpawnWave(waves[currentWave]));

            // Wait until all enemies are dead
            yield return new WaitUntil(() => EnemyManager.AllEnemiesDead);

            currentWave++;
        }

        Debug.Log("ALL WAVES COMPLETE — YOU WIN!");
        SceneManager.LoadScene("start"); 
    }

    IEnumerator SpawnWave(WaveData wave)
    {
        foreach (var entry in wave.enemies)
        {
            for (int i = 0; i < entry.count; i++)
            {
                Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
                EnemyManager.Summon(entry.enemyID, point.position);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
