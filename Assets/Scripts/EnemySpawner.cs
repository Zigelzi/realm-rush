using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 5;
    [SerializeField] [Range(0, 30)] int spawnInterval = 3;
    [SerializeField] bool spawningEnabled = true;

    private GameObject pathParent;
    private GameObject[] objectPool;
    private Transform spawnPosition;

    void Awake()
    {
        GameObject pathParent = GameObject.FindGameObjectWithTag("Path");
        spawnPosition = pathParent.transform.GetChild(0).transform;
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {

        Debug.Log("Starting coroutine");
        StartCoroutine(SpawnEnemies());
    }

    private void PopulatePool()
    {
        objectPool = new GameObject[poolSize];
        for (int i = 0; i < objectPool.Length; i++)
        {
            objectPool[i] = Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation, transform);
            objectPool[i].SetActive(false);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while(spawningEnabled)
        {
            Debug.Log("Coroutine running");
            for (int i = 0; i < objectPool.Length; i++)
            {
                SpawnEnemy(i);
                yield return new WaitForSeconds(spawnInterval);
            }
            
        }
        
    }

    private void SpawnEnemy(int poolPosition)
    {
        if (enemyPrefab != null && objectPool.Length > 0)
        {
            objectPool[poolPosition].SetActive(true);
        }
    }
}
