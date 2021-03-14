using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] [Range(0, 30)] int spawnInterval = 3;
    [SerializeField] bool spawningEnabled = true;

    private GameObject pathParent;
    private Transform spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");
        spawnPosition = pathParent.transform.GetChild(0).transform;

        Debug.Log("Starting coroutine");
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        while(spawningEnabled)
        {
            Debug.Log("Coroutine running");
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
        }
        
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, spawnPosition);
        }
    }
}
