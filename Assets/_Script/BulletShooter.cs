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
    [SerializeField] private float moveSpreadAngle = 3f; // 移動中の最大拡散角度（度数）

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
        // 射線の方向ベクトルを取得
        Vector3 direction = (originalTarget - firePoint.position).normalized;

        // ランダムな角度で回転を加える（上下左右）
        float angleX = Random.Range(-maxAngle, maxAngle);
        float angleY = Random.Range(-maxAngle, maxAngle);
        Quaternion spreadRotation = Quaternion.Euler(angleX, angleY, 0f);

        // 回転を適用して新たなターゲット位置を計算
        Vector3 spreadDirection = spreadRotation * direction;
        Vector3 spreadTarget = firePoint.position + spreadDirection * 100f;

        return spreadTarget;
    }
}