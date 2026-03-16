using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    [SerializeField] private float enemySpawnRate = 4.0f; //in secs
    private float enemySpawnTimer = 0.0f;
    [SerializeField] private float spawnAreaRadius = 5.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        enemySpawnTimer += Time.deltaTime;
        if (enemySpawnTimer >= enemySpawnRate)
        {
            SpawnEnemy();
            enemySpawnTimer = 0.0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector3 spawnPosition = RandomSpawnPositionVector();
        GameObject spawnedEnemy = Instantiate(enemy);
        spawnedEnemy.transform.position = spawnPosition;
    }

    private Vector3 RandomSpawnPositionVector()
    {
        Vector3 randomPosVec = new Vector3(
            this.transform.position.x + Random.Range(-spawnAreaRadius, spawnAreaRadius),
            this.transform.position.y + Random.Range(-spawnAreaRadius, spawnAreaRadius),
            this.transform.position.z + Random.Range(-spawnAreaRadius, spawnAreaRadius)
            );
        return randomPosVec;
    }
}
