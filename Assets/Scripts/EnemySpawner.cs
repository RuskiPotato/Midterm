using UnityEngine;
 
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] enemyPrefabs;   // drag BasicEnemy / FastEnemy prefabs here
    [SerializeField] private Transform player;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float spawnRadius = 9f;
 
    private float timer;
 
    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver) return;
 
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }
 
    private void Spawn()
    {
        Vector2 dir = Random.insideUnitCircle.normalized;
        Vector2 pos = (Vector2)player.position + dir * spawnRadius;
 
        Enemy prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Enemy enemy = Instantiate(prefab, pos, Quaternion.identity);
 
        GameColor color = (GameColor)Random.Range(0, System.Enum.GetValues(typeof(GameColor)).Length);
        enemy.Init(color, player);
    }
}
 