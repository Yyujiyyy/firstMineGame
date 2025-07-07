using System.Collections.Generic;
using UnityEngine;

public class NeutralEnemy : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;          //Enemy��Prefab
    [SerializeField] List<Transform> spawnPoints;
    List<Transform> availablePoints;
    [SerializeField, Range(1, 25)] private int enemyCount = 5; // ���������C���X�y�N�^�[�Ŏw��

    // Start is called before the first frame update
    void Start()
    {
        // �X�|�[���n�_���R�s�[���Ďg�p
        availablePoints = new List<Transform>(spawnPoints);

        // �G�������̓X�|�[���n�_�̐��𒴂��Ȃ��悤�ɂ���
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
