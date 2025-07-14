using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;     // 弾のプレハブ
    [SerializeField] private Transform firePoint;         // 弾の発射位置（銃口など）
    [SerializeField] private GameObject Popup;
    [SerializeField] private GameObject MuzzleFlashPrefab;

    [Header("連射関連")]
    [SerializeField] private float fireRate = 0.2f;
    private float nextFireTime = 0.2f;

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

        // 弾を生成して発射方向を設定
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().Init(firePoint.position, targetPoint);
    }
}