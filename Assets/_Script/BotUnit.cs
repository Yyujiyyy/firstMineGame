using UnityEngine;

public class BotUnit : MonoBehaviour
{
    private BotEnemy manager;
    private Vector3 spawnPos;

    public void Init(BotEnemy mgr)
    {
        manager = mgr;
    }

    public void SetSpawnPointPosition(Vector3 pos)
    {
        spawnPos = pos;
    }

    public void Die()
    {
        manager.OnEnemyDeath(gameObject, spawnPos);
    }
}