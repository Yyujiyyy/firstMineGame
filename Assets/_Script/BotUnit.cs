using UnityEngine;

public class BotUnit : MonoBehaviour
{
    private BotEnemy manager;
    private Vector3 spawnPos;

    private int maxHP = 4;       // 胴体なら4回で死亡
    private int currentHP;

    private bool isDead = false;

    public void Init(BotEnemy mgr)
    {
        manager = mgr;
        currentHP = maxHP;       // 初期体力を設定
        isDead = false;
    }

    public void SetSpawnPointPosition(Vector3 pos)
    {
        spawnPos = pos;
    }

    // ✅ 追加：外部から呼ばれるダメージ処理
    public void TakeDamage(bool isHeadshot)
    {
        if (isDead) return;  // すでに死亡していれば何もしない

        if (isHeadshot)
        {
            Debug.Log("ヘッドショット！");
            Die(isHeadshot);
        }
        else
        {
            currentHP--;

            Debug.Log($"胴体被弾！残りHP: {currentHP}");

            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    public void Die(bool isHeadshot = false)
    {
        if (isDead) return;  // 多重死亡防止

        isDead = true;

        // エフェクトやサウンドの分岐もここで可能
        if (isHeadshot)
        {
            Debug.Log("ヘッドショットで死亡！");
        }
        else
        {
            Debug.Log("通常死亡");
        }

        manager.OnEnemyDeath(gameObject, spawnPos);
    }
}