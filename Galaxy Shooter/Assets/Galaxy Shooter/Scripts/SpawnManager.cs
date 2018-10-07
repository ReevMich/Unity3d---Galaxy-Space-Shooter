using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject playerShipPrefab;
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerupPrefabs;

    public void StartSpawning()
    {
        SpawnPlayer();
        StartCoroutine(SpawnEnemyRoutine(3f));
        StartCoroutine(SpawnPowerupRoutine(5f));
    }

    public void StopSpawning()
    {
        StopAllCoroutines();
        CleanUp();
    }

    private void SpawnPlayer()
    {
        Instantiate(playerShipPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    private IEnumerator SpawnEnemyRoutine(float spawnRate)
    {
        while (true)
        {
            float randomX = Random.Range(-7.9f, 7.9f);
            //respawn back on top with random x position within screen bounds.
            Instantiate(enemyShipPrefab, new Vector3(randomX, 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private IEnumerator SpawnPowerupRoutine(float spawnRate)
    {
        while (true)
        {
            float randomX = Random.Range(-7, 7);
            int randomPowerupIndex = Random.Range(0, powerupPrefabs.Length);
            //respawn back on top with random x position within screen bounds.
            Instantiate(powerupPrefabs[randomPowerupIndex], new Vector3(randomX, 7f, 0), Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
    }

    private void CleanUp()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] powerups = GameObject.FindGameObjectsWithTag("Powerup");
        GameObject[] lasers = GameObject.FindGameObjectsWithTag("Laser");

        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }

        foreach (var powerup in powerups)
        {
            Destroy(powerup.gameObject);
        }

        foreach (var laser in lasers)
        {
            Destroy(laser.gameObject);
        }
    }
}
