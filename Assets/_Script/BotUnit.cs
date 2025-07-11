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

    // ✅ 追加：外部から呼ばれるダメージ処理
    public void TakeDamage(bool isHeadshot)
    {
        // ダメージやHP管理の処理などここに追加可能
        Die(isHeadshot);
    }

    public void Die(bool isHeadshot = false)
    {
        // エフェクトやサウンドの分岐もここで可能
        if (isHeadshot)
        {
            Debug.Log("ヘッドショット！");
        }

        manager.OnEnemyDeath(gameObject, spawnPos);
    }
}