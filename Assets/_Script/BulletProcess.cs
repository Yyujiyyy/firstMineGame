using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class BulletProcess : MonoBehaviour
{
    [Header("パーティクル関連")]
    public Quaternion FinalRotation;
    public ParticleSystem HeadDestroy;   // パーティクル再生用
    public ParticleSystem _Destroy;  // 破壊時のパーティクル

    [SerializeField] private RandomEnemy _randomEnemy;
    [SerializeField] private CountDown50 _countdown;
    private Transform _tr;

    [Header("UI関連")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private EventSystem eventSystem;

    private CheckBox currentRayTarget = null;

    [Header("連射関連")]
    public BulletShooter shot;      //Bulletshooterスクリプトを参照
    private float nextFireTime = 0f;

    [Header("音関連")]
    public AudioClip Sound1;
    private AudioSource _audioSource;
    [SerializeField] private AudioMixer _audioMixer;

    void Start()
    {
        _tr = transform;

        if (mainCamera == null)
            mainCamera = Camera.main;

        _audioSource = GetComponent<AudioSource>();

        SetBgmVolume(1);
        SetSEVolume(12);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + shot.FireRate;

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
                        Generate(hitInfo);
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
                        _audioSource.PlayOneShot(Sound1);       //音
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
                // 通常ヒット（RandomEnemy タグ）
                // ======================
                if (hitObj.CompareTag("RandomEnemy"))
                {
                    _randomEnemy.TakeDamage(false);
                    _audioSource.PlayOneShot(Sound1);       //音
                }

                else if (hitObj.CompareTag("RandomEnemyHead"))
                {
                    _randomEnemy.TakeDamage(true);
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
        var particleObj = Instantiate(HeadDestroy, hitInfo.point, Quaternion.identity);
        var ps = particleObj.GetComponent<ParticleSystem>();      //Play()しなければみえない！！
        ps.Play();

        if (_randomEnemy != null)
        {
            StartCoroutine(_randomEnemy.DieAndRespawn());
            _countdown.DocumentCount();
        }

        Destroy(particleObj.gameObject,  0.2f);
    }

    public void Particle(RaycastHit hitInfo)
    {
        var particleObj =Instantiate(_Destroy, hitInfo.collider.bounds.center, Quaternion.identity);
        var ps = particleObj.GetComponent<ParticleSystem>();

        ps.Play();

        Destroy(particleObj.gameObject, ps.main.duration + 0.5f);
    }

    void SetBgmVolume(float volume)
    {
        _audioSource.volume = volume; // 0.0f ~ 1.0f
    }

    public void SetSEVolume(float dB)
    {
        _audioMixer.SetFloat("SEVolume", dB);
    }
}