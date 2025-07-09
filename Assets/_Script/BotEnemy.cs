using System.Collections.Generic;
using UnityEngine;

public class BotEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField, Range(1, 25)] private int enemyCount = 5;

    private List<Transform> availablePoints;
    private List<Transform> usedPoints = new List<Transform>();
    private List<GameObject> enemyPool = new List<GameObject>();

    //CheckBox関連
    //[SerializeField] private GameObject enemyPrefab;
    //[SerializeField] private Transform spawnPoint;
    [SerializeField] private CheckBox checkBox;  // CheckBox スクリプトを参照

    void Start()
    {
        availablePoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);
            enemy.GetComponent<BotUnit>().Init(this);
            enemyPool.Add(enemy);

            // CheckBoxに追加（必要な場合）
            checkBox.AddToHideObj(enemy);
        }

        int spawnCount = Mathf.Min(enemyCount, availablePoints.Count);
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy()
    {
        if (availablePoints.Count == 0) return;

        GameObject enemy = enemyPool.Find(e => !e.activeSelf);
        if (enemy == null) return;

        int index = Random.Range(0, availablePoints.Count);
        Transform point = availablePoints[index];

        enemy.transform.position = point.position;
        enemy.SetActive(true);
        enemy.GetComponent<BotUnit>().SetSpawnPoint(point);

        usedPoints.Add(point);
        availablePoints.RemoveAt(index);
    }

    // 敵から呼ばれる
    public void OnEnemyDeath(GameObject enemy, Transform spawnPoint)
    {
        enemy.SetActive(false);
        usedPoints.Remove(spawnPoint);
        availablePoints.Add(spawnPoint);

        Invoke(nameof(SpawnEnemy), 0.5f);  // 0.5秒後に再出現（任意）
    }
}