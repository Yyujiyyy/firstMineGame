using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1000f;       // 弾速
    public float maxDistance = 100f;  // 最大到達距離

    private Vector3 direction;         // 弾の進行方向
    private Vector3 startPos;          // 弾の発射位置

    // 弾の初期化（発射位置と方向ベクトルを指定）
    public void InitWithDirection(Vector3 origin, Vector3 dir)
    {
        startPos = origin;
        direction = dir.normalized;
        transform.position = origin;

        // ↓ Cubeを使うので MeshRenderer は不要
        var mesh = GetComponent<MeshRenderer>();
        if (mesh != null) mesh.enabled = false;

        // デバッグ用: 発射方向を赤線で表示
        Debug.DrawRay(origin, direction * 10f, Color.red, 5f);
    }

    void Update()
    {
        // 弾の向きを毎フレーム再計算せず、Initで決めた direction で固定
        transform.position += direction * speed * Time.deltaTime;

        // デバッグ用に軌跡を可視化
        Debug.DrawRay(transform.position, direction * 0.5f, Color.red);

        // 一定距離超えたら弾を破壊
        if (Vector3.Distance(startPos, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Head"))
        {
            var bot = other.GetComponentInParent<BotUnit>();
            if (bot != null)
            {
                bot.TakeDamage(true); // ヘッドショット
            }
        }
        else if (other.CompareTag("BotEnemy"))
        {
            var bot = other.GetComponentInParent<BotUnit>();
            if (bot != null)
            {
                bot.TakeDamage(false); // 胴体
            }
        }

        Destroy(gameObject); // 命中後に弾を消す
    }
}