using UnityEngine;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;     // �e�̃v���n�u
    [SerializeField] private Transform firePoint;         // �e�̔��ˈʒu�i�e���Ȃǁj

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    void Fire()
    {
        // ��ʒ�������Ray���΂�
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

        // �e�𐶐����Ĕ��˕�����ݒ�
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.GetComponent<Bullet>().Init(firePoint.position, targetPoint);
    }
}