using System.Collections.Generic;
using UnityEngine;

public class NeutralEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;          //EnemyのPrefab
    [SerializeField] List<Transform> spawnPoints;
    List<Transform> availablePoints;
    [SerializeField, Range(1, 25)] private int enemyCount = 5; // 生成数をインスペクターで指定

    // Start is called before the first frame update
    void Start()
    {
        // スポーン地点をコピーして使用
        availablePoints = new List<Transform>(spawnPoints);

        // 敵生成数はスポーン地点の数を超えないようにする
        int spawnCount = Mathf.Min(enemyCount, availablePoints.Count);
        for (int i = 0; i < spawnCount; i++)
        {
            int index = Random.Range(0, availablePoints.Count);
            Transform spawnPoint = availablePoints[index];
            Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);
            availablePoints.RemoveAt(index);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
