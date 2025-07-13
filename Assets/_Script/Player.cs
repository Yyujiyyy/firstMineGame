using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public ParticleSystem particle;   // パーティクル再生用
    [SerializeField] private RandomEnemy _randomEnemy;
    [SerializeField] private CountDown50 _countdown;
    private Transform _tr;

    [Header("UI関連")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private EventSystem eventSystem;

    private CheckBox currentRayTarget = null;

    [Header("連射の銃弾関連")]
    [SerializeField] private float fireRate = 0.2f; // 🔫 連射間隔（秒）
    private float nextFireTime = 0f;                // ⏱ 次に発射可能な時間

    void Start()
    {
        _tr = transform;

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        // ======================
        // 左クリック長押し対応（GetMouseButton）
        // ======================
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate; // 次の発射時間を更新

            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
            {
                GameObject hitObj = hitInfo.collider.gameObject;

                // ======================
                // ヘッドショット（Head タグ）
                // ======================
                if (hitObj.CompareTag("Head"))
                {
                    BotUnit unit = hitObj.GetComponentInParent<BotUnit>();
                    if (unit != null)
                    {
                        unit.TakeDamage(true);  // 🎯 ヘッドショット
                    }
                }
                // ======================
                // 通常ヒット（BotEnemy タグ）
                // ======================
                else if (hitObj.CompareTag("BotEnemy"))
                {
                    BotUnit unit = hitObj.GetComponent<BotUnit>();
                    if (unit != null)
                    {
                        unit.TakeDamage(false);  // 🔘 通常攻撃
                    }
                }

                // ======================
                // UI表示切り替え（CheckBox タグ）
                // ======================
                if (hitObj.CompareTag("CheckBox"))
                {
                    CheckBox target = hitObj.GetComponent<CheckBox>();
                    if (target != null)
                    {
                        if (target == currentRayTarget)
                        {
                            target.ToggleUI(); // 同じ対象を再度クリック：トグル
                        }
                        else
                        {
                            if (currentRayTarget != null)
                                currentRayTarget.HideUI(); // 前のUIを非表示

                            target.ToggleUI();
                            currentRayTarget = target;
                        }
                    }
                }

                // ======================
                // 敵生成（RandomEnemy タグ）
                // ======================
                if (hitObj.CompareTag("RandomEnemy"))
                {
                    Generate(hitInfo);
                }
            }
            else
            {
                // 何もヒットしてない → UI を非表示にする（任意）
                if (currentRayTarget != null)
                {
                    currentRayTarget.HideUI();
                    currentRayTarget = null;
                    Debug.Log("UI非表示：何もヒットしなかった");
                }
            }
        }

        // ▼（参考）クリック1回ごとの処理を行いたい場合はこちらを使う：
        // if (Input.GetMouseButtonDown(0)) { ... }
    }

    public void Generate(RaycastHit hitInfo)
    {
        Instantiate(particle, hitInfo.point, Quaternion.identity);

        if (_randomEnemy != null)
        {
            _randomEnemy.EnemyGenerate();
            _countdown.DocumentCount();
        }
    }
}