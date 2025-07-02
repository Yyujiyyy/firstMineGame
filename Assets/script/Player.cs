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
            // UIボタンに対するRay判定
            // ================================

            // PointerEventDataを生成し、画面中心座標をセット（またはInput.mousePositionでもOK）
            PointerEventData pointerData = new PointerEventData(eventSystem);
            pointerData.position = new Vector2(Screen.width / 2, Screen.height / 2);  // 画面中央からRayを出す

            List<RaycastResult> uiResults = new List<RaycastResult>();
            uiRaycaster.Raycast(pointerData, uiResults);

            foreach (RaycastResult result in uiResults)
            {
                Button btn = result.gameObject.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.Invoke(); // ボタンのクリックイベントを実行
                    return; // ボタンがクリックされた場合、以降の処理は行わない
                }
            }

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
            }
        }
    }
}