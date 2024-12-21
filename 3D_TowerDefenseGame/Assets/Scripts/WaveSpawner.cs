using UnityEngine;
using Unity.Collections;
using System.Collections;
using UnityEngine.UI;
using System;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefabs;
    public float timeBeetwenWaves = 5f;
    private float countdown = 2f;
    private int waveIndex = 0;
    public Transform spawnPoint;
    public Text waveCountdownTimer;
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    public GameManager gameManager;

    // Update is called once per frame
    void Update()
    {
        if(EnemiesAlive>0)
        {
            return;
        }

        if (waveIndex == waves.Length)
        {
            gameManager.WinLevel();
            enabled = false;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave()); 
            // Coroutine'ler, bir fonksiyonun belirli noktalarýnda duraklatýlýp daha sonra kaldýðý yerden devam etmesini saðlar.
            countdown = timeBeetwenWaves;
            return;
        }
        countdown-=Time.deltaTime;
        // countdown'u 0 ile +sonsuz arasýnda kýskaca alýr.
        countdown = Mathf.Clamp(countdown,0f,Mathf.Infinity);
        waveCountdownTimer.text=string.Format("{0:00.00}",countdown);
    }

    IEnumerator SpawnWave() // Coroutine yöntemi denir.
    {
        PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];
        EnemiesAlive = wave.count;

        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.enemy);
            yield return new WaitForSeconds(1f/wave.rate); // Düþmanlar arasýndaki süreyi belirler.
        }

        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
