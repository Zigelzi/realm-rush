using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] int poolSize = 5;
    [SerializeField] [Range(0.5f, 30f)] float spawnInterval = 3f;
    [SerializeField] bool spawningEnabled = true;

    GameObject pathParent;
    GameObject[] objectPool;
    Transform spawnPosition;
    GameManager gameManager;

    void Awake()
    {
        pathParent = GameObject.FindGameObjectWithTag("Path");
        gameManager = FindObjectOfType<GameManager>();
        spawnPosition = pathParent.transform.GetChild(0).transform;
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    void PopulatePool()
    {
        objectPool = new GameObject[poolSize];
        for (int i = 0; i < objectPool.Length; i++)
        {
            objectPool[i] = Instantiate(enemyPrefab, spawnPosition.position, spawnPosition.rotation, transform);
            objectPool[i].SetActive(false);
        }
    }

    IEnumerator SpawnEnemies()
    {
        while(spawningEnabled && gameManager.GameState != GameManager.State.Defeated)
        {
            for (int i = 0; i < objectPool.Length; i++)
            {
                SpawnEnemy(i);
                yield return new WaitForSeconds(spawnInterval);
            }
            
        }
        
    }

    void SpawnEnemy(int poolPosition)
    {
        if (enemyPrefab != null && objectPool.Length > 0)
        {
            objectPool[poolPosition].SetActive(true);
        }
    }
}
