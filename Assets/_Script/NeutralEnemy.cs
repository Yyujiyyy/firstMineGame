using System.Collections.Generic;
using UnityEngine;

public class NeutralEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;          //Enemy‚ÌPrefab
    [SerializeField] List<Transform> spawnPoints;
    List<Transform> availablePoints;

    [SerializeField] public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        availablePoints = new List<Transform>(spawnPoints);

        int enemyCount = Mathf.Min(24, availablePoints.Count);
        for (int i = 0; i < enemyCount; i++)
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
