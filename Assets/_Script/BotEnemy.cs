using System.Collections.Generic;
using UnityEngine;

public class BotEnemy : MonoBehaviour
{
    [Header("スポーン設定")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField, Range(1, 25)] private int enemyCount = 5;
    [SerializeField] private Transform spawnOrigin;
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(10f, 10f); // X-Z方向の範囲
    [SerializeField] private float spacing = 2f;

    [Header("連携コンポーネント")]
    [SerializeField] private CheckBox checkBox;

    private List<Vector3> spawnPoints = new List<Vector3>();
    private List<Vector3> availablePoints = new List<Vector3>();
    private List<Vector3> usedPoints = new List<Vector3>();
    private List<GameObject> enemyPool = new List<GameObject>();

    void Start()
    {
        GenerateSpawnPoints();

        availablePoints = new List<Vector3>(spawnPoints);

        // 敵プールを準備
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.SetActive(false);

            if (enemy.TryGetComponent<BotUnit>(out var bot))
                bot.Init(this);

            enemyPool.Add(enemy);
            checkBox?.AddToHideObj(enemy);
        }

        // 出現開始
        int spawnCount = Mathf.Min(enemyCount, availablePoints.Count);
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnEnemy();
        }
    }

    void GenerateSpawnPoints()
    {
        if (spawnOrigin == null)
        {
            Debug.LogError("spawnOrigin が指定されていません");
            return;
        }

        Vector3 origin = spawnOrigin.position;
        for (float x = -spawnAreaSize.x; x <= spawnAreaSize.x; x += spacing)
        {
            for (float z = -spawnAreaSize.y; z <= spawnAreaSize.y; z += spacing)
            {
                Vector3 point = origin + new Vector3(x, 0, z);
                spawnPoints.Add(point);
            }
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogWarning("生成されたスポーンポイントが0です。spacingが大きすぎる可能性があります。");
        }
    }

    public void SpawnEnemy()
    {
        if (availablePoints.Count == 0) return;

        GameObject enemy = enemyPool.Find(e => !e.activeSelf);
        if (enemy == null) return;

        int index = Random.Range(0, availablePoints.Count);
        Vector3 spawnPos = availablePoints[index];

        enemy.transform.position = spawnPos;
        enemy.transform.rotation = Quaternion.identity;
        enemy.SetActive(true);

        if (enemy.TryGetComponent<BotUnit>(out var bot))
            bot.SetSpawnPointPosition(spawnPos);

        usedPoints.Add(spawnPos);
        availablePoints.RemoveAt(index);
    }

    public void OnEnemyDeath(GameObject enemy, Vector3 spawnPos)
    {
        enemy.SetActive(false);

        if (usedPoints.Contains(spawnPos))
        {
            usedPoints.Remove(spawnPos);
            availablePoints.Add(spawnPos);
        }

        Invoke(nameof(SpawnEnemy), 0.5f);
    }

    //void OnDrawGizmosSelected()
    //{
    //    // ギズモ表示色（未使用＝緑、使用中＝赤）
    //    if (Application.isPlaying)
    //    {
    //        // ゲーム中：状態別に色分け
    //        Gizmos.color = Color.green;
    //        foreach (Vector3 point in availablePoints)
    //        {
    //            Gizmos.DrawSphere(point, 0.3f);
    //        }

    //        Gizmos.color = Color.red;
    //        foreach (Vector3 point in usedPoints)
    //        {
    //            Gizmos.DrawSphere(point, 0.3f);
    //        }
    //    }
    //    else
    //    {
    //        // エディタ上（プレイ前）：基準から仮描画
    //        if (spawnOrigin == null) return;

    //        Vector3 origin = spawnOrigin.position;
    //        for (float x = -spawnAreaSize.x; x <= spawnAreaSize.x; x += spacing)
    //        {
    //            for (float z = -spawnAreaSize.y; z <= spawnAreaSize.y; z += spacing)
    //            {
    //                Vector3 pos = origin + new Vector3(x, 0, z);
    //                Gizmos.color = Color.gray;
    //                Gizmos.DrawWireSphere(pos, 0.2f);
    //            }
    //        }
    //    }
    //}
}