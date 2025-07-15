using UnityEngine;

public class BotUnit : MonoBehaviour
{
    private BotEnemy manager;
    private Vector3 spawnPos;

    private int maxHP = 4;       // 胴体なら4回で死亡
    private int currentHP;

    private bool isDead = false;
    private bool isInitialized = false;     // Init()を一度しか通さない制御用

    // ===========================
    // 再出現時の初期化処理
    // ===========================
    void OnEnable()
    {
        currentHP = maxHP;      // HP全回復
        isDead = false;         // 死亡フラグ解除
        isInitialized = false;  // Initの再許可（再生成時のマネージャー再設定用）

        //Debug.Log($"OnEnableでHP初期化: {currentHP}");
    }

    // ===========================
    // マネージャーから最初に呼ばれる初期化処理
    // ===========================
    public void Init(BotEnemy mgr)
    {
        if (isInitialized) return; // 2回目以降はスキップ

        manager = mgr;
        currentHP = maxHP;       // 念のため再設定
        isDead = false;

        isInitialized = true;    // 初期化済みフラグ

        //Debug.Log($"Init実行: HP = {currentHP}");
    }

    // ===========================
    // スポーン位置の記録（再出現に使用）
    // ===========================
    public void SetSpawnPointPosition(Vector3 pos)
    {
        spawnPos = pos;

        // ※必要に応じてここで isInitialized = false にしてもOKだが、
        // OnEnableで済むなら不要（重複リセット防止のため）
    }

    // ===========================
    // 外部から呼ばれるダメージ処理
    // ===========================
    public void TakeDamage(bool isHeadshot)
    {
        if (isDead) return;  // 死亡済みなら無視

        if (isHeadshot)
        {
            //Debug.Log("ヘッドショット！");
            Die(true); // 即死
        }
        else
        {
            currentHP--;
            //Debug.Log($"胴体被弾！残りHP: {currentHP}");

            if (currentHP <= 0)
            {
                Die(); // 通常死亡
            }
        }
    }

    // ===========================
    // 死亡処理
    // ===========================
    public void Die(bool isHeadshot = false)
    {
        if (isDead) return;  // 多重死亡防止

        isDead = true;

        if (isHeadshot)
        {
            //Debug.Log("ヘッドショットで死亡！");
        }
        else
        {
            //Debug.Log("通常死亡");
        }

        manager.OnEnemyDeath(gameObject, spawnPos); // マネージャーに通知
    }
}