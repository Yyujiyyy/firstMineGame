using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    public ParticleSystem particle;   // パーティクル再生用
    [SerializeField] public Transform enemy;
    [SerializeField] private RandomEnemy _randomEnemy;
    [SerializeField] private CountDown50 _countdown;
    private Transform _tr;

    [Header("UI関連")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private EventSystem eventSystem;

    private CheckBox currentRayTarget = null;

    void Start()
    {
        _tr = transform;

        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
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
                        unit.TakeDamage(true);  // ヘッドショット
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
                        unit.TakeDamage(false);  // 通常攻撃
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
                            target.ToggleUI();
                        }
                        else
                        {
                            if (currentRayTarget != null)
                                currentRayTarget.HideUI();

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