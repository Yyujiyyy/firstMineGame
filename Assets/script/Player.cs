using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private EnemyController _random;
    public ParticleSystem particle;   // Hierarchy上のParticleSystemを指定
    [SerializeField] public Transform enemy;
    Transform _tr;

    // UI関連（Inspectorで設定）
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private EventSystem eventSystem;

    // RayでヒットしたUI対象を記録（連続して同じ対象を処理しないようにする）
    private CheckBox currentRayTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("enemy").transform;

        _random = GameObject.FindObjectOfType<EnemyController>();
        //これでシーン内の最初に見つかった EnemyController を _random にセットできる。

        _tr = transform;

        // カメラが未設定なら自動で取得
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //マウスを左クリックしたとき
        if (Input.GetMouseButtonDown(0))
        {
            // ================================
            // 通常の3D空間へのRay判定
            // ================================

            //新しいRayを作る。
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            //Rayの発射地点               ,方向

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
            {
                if (hitInfo.collider.CompareTag("enemy"))
                {
                    //particle.transform.position = hitInfo.point;       //particleの位置を当たった位置にする（ワープ先についてこない）
                    //instantiateするから必要ない↑
                    Instantiate(particle, hitInfo.point, Quaternion.identity);

                    if (_random != null)                    //nullチェック      解説
                    {
                        _random.EnemyGenerate();            //_random に入っているオブジェクトに対して EnemyGenerate() を呼び出す
                    }
                }

                // ================================
                // レイが "CheckBox" に当たった場合 UI 表示
                // ================================
                if (hitInfo.collider.CompareTag("CheckBox"))
                {
                    CheckBox target = hitInfo.collider.GetComponent<CheckBox>();
                    if (target != null && target != currentRayTarget)
                    {
                        if (currentRayTarget != null)
                            currentRayTarget.HideUI(); // 前のターゲットのUIを非表示

                        target.ShowUI();               // 今のターゲットのUIを表示
                        currentRayTarget = target;     // 記録更新
                    }
                }
            }
            else
            {
                //// Rayが何にも当たらなかった場合、UIを隠す
                //if (currentRayTarget != null)
                //{
                //    currentRayTarget.HideUI();
                //    currentRayTarget = null;
                //    Debug.Log("なにもない");
                //}
            }
        }
    }
}