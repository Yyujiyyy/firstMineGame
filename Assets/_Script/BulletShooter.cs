using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;     // 弾のプレハブ
    [SerializeField] private Transform firePoint;         // 弾の発射位置（銃口など）
    [SerializeField] private GameObject Popup;            // 設定画面
    [SerializeField] private GameObject MuzzleFlashPrefab;// マズルフラッシュ

    [Header("連射関連")]
    [SerializeField] public float fireRate = 0.1f;
    private float nextFireTime = 0f;

    [Header("銃声関連")]
    public AudioClip sound1;
    AudioSource audioSource;

    [Header("スプレッド関連")]
    [SerializeField] private float moveSpreadAngle = 10f; // 移動中の最大拡散角度（度数）

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Popup == null || Popup.activeSelf) return;

        // 左クリック長押し＆連射制御
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;

            GameObject flash = Instantiate(MuzzleFlashPrefab, firePoint.position, Quaternion.identity);
            flash.transform.SetParent(firePoint);

            Fire();
            audioSource.PlayOneShot(sound1);

            Destroy(flash, 0.2f);
        }
    }

    void Fire()
    {
        // 画面中央からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));     //randomにする
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * 100f;
        }

        // 🔽 プレイヤーが動いている場合、ターゲットポイントにスプレッドを加える
        if (PlayerControl.Instance != null && PlayerControl.Instance.IsMoving)
        {
            targetPoint = ApplySpread(targetPoint, moveSpreadAngle);
        }

        // 🔧 弾の出現位置を firePoint の前方に少しオフセットする
        Vector3 spawnPosition = firePoint.position + firePoint.forward * 0.2f;

        // 弾を生成して発射方向を設定
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().Init(spawnPosition, targetPoint);
    }

    // スプレッド角度の範囲内でターゲット位置をずらす
    Vector3 ApplySpread(Vector3 originalTarget, float maxAngle)
    {
        Vector3 direction = (originalTarget - firePoint.position).normalized;

        // nullチェックを追加（念のため）
        if (Camera.main == null)
        {
            Debug.LogWarning("Camera.main is null");
            return originalTarget;
        }

        Vector3 right = Camera.main.transform.right;
        Vector3 up = Camera.main.transform.up;

        // Spread値生成
        Vector2 spread = Random.insideUnitCircle * Mathf.Tan(maxAngle * Mathf.Deg2Rad);
        Vector3 spreadDirection = direction + right * spread.x + up * spread.y;
        spreadDirection.Normalize();

        Vector3 result = firePoint.position + spreadDirection * 100f;

        if (float.IsNaN(result.x) || float.IsNaN(result.y) || float.IsNaN(result.z))
        {
            Debug.LogWarning("SpreadTarget is NaN!");
            return originalTarget;
        }

        // デバッグ用に描画（赤色の線）
        Debug.DrawRay(firePoint.position, spreadDirection * 10f, Color.red, 0.5f);

        return result;
    }
}