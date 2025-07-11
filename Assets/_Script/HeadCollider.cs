using UnityEngine;

public class HeadCollider : MonoBehaviour
{
    private BotUnit botUnit;

    void Start()
    {
        // 親オブジェクトの BotUnit を取得
        botUnit = GetComponentInParent<BotUnit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 例：弾に "Bullet" タグが付いている場合
        if (other.CompareTag("Bullet"))
        {
            // 即死処理
            botUnit?.Die(true); // ヘッドショット = true

            // 弾も消すならここで Destroy
            Destroy(other.gameObject);
        }
    }
}