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
    [Header("Valorant感度変換")]
    [SerializeField] private float valorantSensitivity = 0.4f; // Valorant上の感度（例：0.4）
    [SerializeField] private float mouseDPI = 800f;            // 実際のマウスDPI（例：800）

    // UIでDPIを入力させる用のTextMeshPro InputField
    [Header("UI入力")]
    [SerializeField] private TMP_InputField dpiInputField;

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

    // Start is called before the first frame update
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
    }

    // 毎フレームでの視点移動・カーソルロック処理
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
        float xRot = Input.GetAxisRaw("Mouse X") * Sensitivity;
        float yRot = Input.GetAxisRaw("Mouse Y") * Sensitivity;
        //マウスの移動量　       ×　   感度

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);
        characterRot *= Quaternion.Euler(0, xRot, 0);

        cameraRot = ClampRotation(cameraRot);

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;

        // ④ カーソルロック処理（マウスクリックなど）
        if (Popup == null || !Popup.activeSelf)
        {
            UpdateCursorLock();
        }
    }

    // Rigidbodyを使った移動とジャンプ処理
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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * 7f, ForceMode.Impulse);
            //一瞬の衝撃力を加える

            isGrounded = false;
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

        // ※Escapeの処理は削除！

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
            if (rb == null) return false;

            Vector3 horizontalVelocity = rb.velocity;
            horizontalVelocity.y = 0f; // Y軸は無視（ジャンプ中でも動いてるとみなさない）
            return horizontalVelocity.magnitude > 0.05f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            isGrounded = true;
    }
}