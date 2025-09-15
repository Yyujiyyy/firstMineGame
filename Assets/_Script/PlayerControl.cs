using UnityEngine;
using TMPro; // TextMeshProのUI（InputField）を使うため

public class PlayerControl : MonoBehaviour
{
    float x, z;
    public float speed = 0.1f;

    public GameObject cam;
    Quaternion cameraRot, characterRot;
    [Range(0.0001f, 40.000f)][SerializeField] public float Sensitivity = 1f;
    bool cursorLock = true;
    float minX = -90, maxX = 90f;

    [SerializeField] public GameObject Popup;

    // Jump関連
    Rigidbody rb;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.3f;
    private bool isGrounded;

    // Valorant感度変換用の設定
    //[Header("Valorant感度変換")]
    [SerializeField] private float valorantSensitivity = 0.4f; // Valorant上の感度（例：0.4）
    [SerializeField] private float mouseDPI = 800f;            // 実際のマウスDPI（例：800）

    //移動時の視覚フィードバック
    float speeed = 6f;

    // UIでDPIを入力させる用のTextMeshPro InputField
    [Header("UI入力")]
    [SerializeField] private TMP_InputField dpiInputField;

    [Header("銃")]
    [SerializeField] private GameObject Gun;
    [Tooltip("銃の初期位置")] private Vector3 gunStartPos;

    [Header("足音")]
    [Tooltip("足音"),SerializeField] private AudioClip Sound1;
    [SerializeField] private AudioSource _audioSource;
    private bool footstepPlayed = false;

    // 処理の有効・無効を管理するフラグ
    private bool isActive = true;  // trueなら処理実行、falseなら処理停止

    //インスタンス化？
    public static PlayerControl Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;

        // Sensitivity（感度）をPlayerPrefsからロード（なければ計算して保存）
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            Sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        }
        else
        {
            Sensitivity = ConvertValorantToUnitySensitivity(valorantSensitivity, mouseDPI);
            PlayerPrefs.SetFloat("Sensitivity", Sensitivity);
        }

        // Rigidbodyの参照取得
        rb = GetComponent<Rigidbody>();

        // 視点ロック
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gunStartPos = Gun.transform.localPosition; // 初期位置を保存
    }

    void Update()
    {
        // 処理停止中なら何もしない
        if (!isActive) return;

        if (Popup == null)
        {
            Debug.LogWarning("Popup が null または DestroyされたのでUpdateを終了します。");
            isActive = false;  // Popupがなくなったので以降処理を止める
            return;
        }

        // ① EscapeキーでPopup開閉
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool willShow = !Popup.activeSelf;
            Popup.SetActive(willShow);

            if (!willShow)
            {
                // Popupを閉じた瞬間：カーソルをロック＆非表示にする
                cursorLock = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                // Popupを開いた瞬間：カーソルをアンロック＆表示
                cursorLock = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        // ② Popupが表示中なら、視点処理やカーソルロックをスキップ
        if (Popup.activeSelf)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return;
        }

        // ③ マウスによる視点回転
        float mouseX = Input.GetAxis("Mouse X") * Sensitivity * Time.deltaTime * 100f;
        float mouseY = Input.GetAxis("Mouse Y") * Sensitivity * Time.deltaTime * 100f;
        //マウスの移動量　       ×　   感度

        x += mouseX;
        z -= mouseY;
        z = Mathf.Clamp(z, minX, maxX);

        cam.transform.localRotation = Quaternion.Euler(z, 0f, 0f);
        transform.localRotation = Quaternion.Euler(0f, x, 0f);

        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        // ④ カーソルロック処理（マウスクリックなど）
        if (Popup == null || !Popup.activeSelf)
        {
            UpdateCursorLock();
        }

        // 🔽 移動時の銃揺れ（GunBob）
        if (IsMoving)
        {
            // 左右揺れ (sin波)
            float xOffset = Mathf.Sin(Time.time * speeed) * 0.03f;

            // 上下揺れ (cos波、少し速くて小さめ)
            float yOffset = Mathf.Cos(Time.time * speeed * 2f) * 0.025f;

            // 初期位置からのオフセット
            Gun.transform.localPosition = gunStartPos + new Vector3(xOffset, yOffset, 0f);

            if (!footstepPlayed)
            {
                footstepPlayed = true;
                Invoke(nameof(MoveSound), 0.2f); // 0.2秒後に1回だけ鳴らす
            }   

        }
        else
        {
            // 移動していない時は初期位置に戻す
            // ワープ防止：補間して動かす
            Gun.transform.localPosition = Vector3.Lerp(Gun.transform.localPosition, gunStartPos,
                Time.deltaTime * 10f  // 数値大きいほど追従が速い
            );

            footstepPlayed = false;  // 止まったらフラグリセット
            _audioSource.Stop();
        }

        //Debug.Log(IsMoving);      isMovingが動いているかどうか
    }

    private void FixedUpdate()
    //毎フレームではなく、”物理演算の更新タイミング”で呼ばれる関数
    //このスクリプトで FixedUpdate() を使う理由は、「物理ベースの動き（Rigidbodyなしでも）」を安定して実行するため
    {
        if (!isActive) return;  // 処理停止中なら何もしない

        if (Popup != null && Popup.activeSelf)
        {
            return;
        }

        // 🔽 camがnullまたは破棄されたらFixedUpdateを中断
        if (cam == null)
        {
            Debug.LogWarning("cam が Destroy されているため、FixedUpdate をスキップします。");
            return;
        }

        float moveX = Input.GetAxisRaw("Horizontal") * speed;
        float moveZ = Input.GetAxisRaw("Vertical") * speed;

        Vector3 forward = cam.transform.forward;
        Vector3 right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDir = forward * moveZ + right * moveX;
        Vector3 targetPos = rb.position + moveDir * Time.fixedDeltaTime;

        rb.MovePosition(targetPos);

        //isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundDistance, groundLayer);

        // =========================
        // ジャンプ処理
        // =========================
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // 上方向リセット
            rb.AddForce(Vector3.up * 7f, ForceMode.Impulse);             // ジャンプ力
            isGrounded = false;                                          // 瞬間的に地面フラグを下げる
        }

        // =========================
        // 落下を速くする処理（Better Fall）
        // =========================
        float fallMultiplier = 2.5f; // 落下速度を速くする倍率
        if (rb.velocity.y < 0)       // 下降中のみ
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
            // Physics.gravity.y は負の値なので、y方向の速度に加算して落下を速める
        }
    }

    // エスケープキーとクリックでカーソルロック切り替え
    public void UpdateCursorLock()
    {
        if (!isActive) return;  // 処理停止中なら何もしない

        if (Popup == null)
        {
            Debug.LogWarning("Popup が null または Destroyされています。UpdateCursorLockを終了します。");
            isActive = false;  // Popupがなくなったので処理停止
            return;
        }

        // Popupが表示中なら、カーソルは常にフリー状態
        if (Popup.activeSelf)
        {
            cursorLock = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            return; // ロック処理をスキップ
        }

        // 左クリックでロック再開
        if (Input.GetMouseButton(0))
        {
            cursorLock = true;
        }

        // 状態に応じて反映
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    // 視点回転をX軸だけに制限（上下見すぎないようにする）
    public Quaternion ClampRotation(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        angleX = Mathf.Clamp(angleX, minX, maxX);

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }

    // =============================
    // 🧮 Valorant感度 → Unity感度換算式
    // =============================
    private float ConvertValorantToUnitySensitivity(float valorantSens, float dpi)
    {
        // 0.000875 は実測ベースの係数（Unityの回転挙動に合わせて調整）
        return valorantSens * dpi * 0.000875f;
    }

    // =============================
    // 💡 UIからDPIを入力 → 感度を更新する関数
    // =============================
    public void UpdateDPIFromInput()
    {
        if (float.TryParse(dpiInputField.text, out float dpi))
        {
            mouseDPI = dpi;
            Sensitivity = ConvertValorantToUnitySensitivity(valorantSensitivity, mouseDPI);
            PlayerPrefs.SetFloat("Sensitivity", Sensitivity); // 保存
            Debug.Log($"DPI更新: {dpi} → 感度: {Sensitivity}");
        }
        else
        {
            Debug.LogWarning("DPI入力が数値ではありません");
        }
    }

    // ポップアップを破棄したりシーン遷移などでPlayerControlを無効化する時に呼ぶ想定
    public void DisablePlayerControl()
    {
        isActive = false;

        if (Popup != null)
        {
            Destroy(Popup);
            Popup = null;
        }
    }

    //カーソルロックメソッド
    public void LockCursor()
    {
        cursorLock = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // =============================
    // 🎯 プレイヤーが移動しているか判定（スプレッド用）
    // =============================
    public bool IsMoving
    {
        get
        {
            float hor = Input.GetAxisRaw("Horizontal");
            float ver = Input.GetAxisRaw("Vertical");
            return (hor != 0f || ver != 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;
    }

    // 🔹 銃揺れを外部から取得
    public Vector3 GunBobOffset
    {
        get
        {
            if (!IsMoving) return Vector3.zero;
            float xOffset = Mathf.Sin(Time.time * speeed) * 0.03f;
            float yOffset = Mathf.Cos(Time.time * speeed * 2f) * 0.025f;
            return new Vector3(xOffset, yOffset, 0f);
        }
    }

    void MoveSound()
    {
        _audioSource.loop = true;
        _audioSource.Play();
    }
}