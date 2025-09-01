using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;      // 弾のプレハブ
    [SerializeField] private Transform _firePoint;          // 発射位置
    [SerializeField] private GameObject _popup;             // 設定画面
    [SerializeField] private GameObject _muzzleFlashPrefab; // マズルフラッシュ

    [Header("連射関連")]
    [SerializeField] public float FireRate = 0.1f;
    private float nextFireTime = 0f;

    [Header("銃声関連")]
    public AudioClip Sound1;
    private AudioSource _audioSource;

    [Header("スプレッド関連")]
    [SerializeField] private float _moveSpreadAngle = 10f; // 移動中のスプレッド角度（度数）

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        Audio(0.1f);
    }

    void Update()
    {
        if (_popup == null || _popup.activeSelf) return;

        // 左クリック長押し＆連射制御
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + FireRate;

            // マズルフラッシュ生成
            GameObject flash = Instantiate(_muzzleFlashPrefab, _firePoint.position, Quaternion.identity);
            flash.transform.SetParent(_firePoint);

            Fire();
            _audioSource.PlayOneShot(Sound1);

            Destroy(flash, 0.2f);
        }
    }

    void Fire()
    {
        // 画面中央からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * 100f;
        }

        Vector3 bulletDirection;

        // プレイヤーが移動中ならスプレッドを加える
        if (PlayerControl.Instance != null && PlayerControl.Instance.IsMoving)
        {
            bulletDirection = ApplySpreadDirection(targetPoint, _moveSpreadAngle);
        }
        else
        {
            bulletDirection = (targetPoint - _firePoint.position).normalized;
        }

        // 弾の出現位置を少し前方にオフセット
        Vector3 spawnPos = _firePoint.position + _firePoint.forward * 0.2f;

        // 弾生成、方向ベクトルで初期化
        GameObject bullet = Instantiate(_bulletPrefab);
        bullet.GetComponent<Bullet>().InitWithDirection(spawnPos, bulletDirection);

        // デバッグ用: 緑線でスプレッド方向を表示
        Debug.DrawLine(spawnPos, spawnPos + bulletDirection * 50f, Color.green, 1f);
    }

    // スプレッド角度の範囲内で方向ベクトルを返す
    Vector3 ApplySpreadDirection(Vector3 target, float maxAngle)
    {
        Vector3 dir = (target - _firePoint.position).normalized;

        // nullチェックを追加（念のため）
        if (Camera.main == null)
        {
            Debug.LogWarning("Camera.main is null");
            return dir;
        }

        Vector3 right = Camera.main.transform.right;    //キャッシュすべき  
        Vector3 up = Camera.main.transform.up;

        // スプレッド値生成
        Vector2 spread = Random.insideUnitCircle * Mathf.Tan(maxAngle * Mathf.Deg2Rad);
        Vector3 spreadDir = dir + right * spread.x + up * spread.y;
        spreadDir.Normalize();

        // デバッグ用: 赤線でスプレッド方向を表示
        Debug.DrawRay(_firePoint.position, spreadDir * 10f, Color.red, 0.5f);

        return spreadDir;
    }

    void Audio(float volume)
    {
        _audioSource.volume = volume;
    }
}