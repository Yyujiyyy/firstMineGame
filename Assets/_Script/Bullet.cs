using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 1000f;
    public float maxDistance = 100f;

    private Vector3 direction;
    private Vector3 startPos;

    public void Init(Vector3 origin, Vector3 target)
    {
        startPos = origin;
        direction = (target - origin).normalized;
        transform.position = origin;

        //↓ Cubeを使うのでMeshRendererの制御は不要のためコメントアウト
        var mesh = GetComponent<MeshRenderer>();
        if (mesh != null) mesh.enabled = false;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(startPos, transform.position) > maxDistance)
        {
            Destroy(gameObject); // 一定距離超えたら削除
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
            var bot = other.GetComponent<BotUnit>();
            if (bot != null)
            {
                bot.TakeDamage(false); // 胴体
            }
        }

        Destroy(gameObject); // 命中後に弾を消す
    }
}