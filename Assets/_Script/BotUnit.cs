using UnityEngine;

public class BotUnit : MonoBehaviour
{
    private BotEnemy manager;
    private Transform mySpawnPoint;

    public void Init(BotEnemy mgr)
    {
        manager = mgr;
    }

    public void SetSpawnPoint(Transform point)
    {
        mySpawnPoint = point;
    }

    public void Die()
    {
        manager.OnEnemyDeath(this.gameObject, mySpawnPoint);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Die();
        }
    }
}