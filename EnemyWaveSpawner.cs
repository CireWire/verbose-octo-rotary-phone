using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    public List<WaveConfig> waveConfigs;
    public float startDelay = 2.0f;

    void Start()
    {
        StartCoroutine(SpawnAllWaves());
    }

    IEnumerator SpawnAllWaves()
    {
        while (true)
        {
            foreach (WaveConfig wave in waveConfigs)
            {
                for (int i = 0; i < wave.enemyCount; i++)
                {
                    GameObject newEnemy = Instantiate(wave.enemyPrefab, transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(wave.timeBetweenSpawns);
                }
            }
        }
    }

    IEnumerator StartWaveSpawning()
    {
        yield return new WaitForSeconds(startDelay);
        StartCoroutine(SpawnAllWaves());
    }
}
