using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] AudioClip[] zombieSounds;

    AudioSource myAudioSource;

    IEnumerator Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (looping);

    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveCount = startingWave; waveCount < waveConfigs.Count; waveCount++)
        {
            AudioClip clip = zombieSounds[UnityEngine.Random.Range(0, zombieSounds.Length)];
            AudioSource.PlayClipAtPoint(clip, this.transform.position);
            var currentWave = waveConfigs[waveCount];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }

    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(),
                        new Vector3(waveConfig.GetWaypoints()[0].transform.position.x, waveConfig.GetWaypoints()[0].transform.position.y, waveConfig.GetWaypoints()[0].transform.position.z),
                Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }

}
