using UnityEngine;

public class FPScamera : MonoBehaviour
{
    float x, z;
    public float speed = 0.1f;

    public GameObject cam;          
    Quaternion cameraRot, characterRot;     
    [Range(0.0001f, 40.000f)][SerializeField]public float Sensitivity = 1f;
    bool cursorLock = true;         
    float minX = -90, maxX = 90f;

    [SerializeField] public GameObject Popup;


    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            Sensitivity = PlayerPrefs.GetFloat("Sensitivity");      //Sensitivity変数を共通の数値にする
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Popup.activeSelf)
        {
            return;                                            // 視点処理を止める（以下のUpdate処理をすべてスキップ）
        }

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            Sensitivity = PlayerPrefs.GetFloat("Sensitivity"); // 毎フレーム取得
        }

        float xRot = Input.GetAxisRaw("Mouse X") * Sensitivity;
        float yRot = Input.GetAxisRaw("Mouse Y") * Sensitivity;
        //マウスの移動量　       ×　   感度

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);            //４元数をオイラー角を使ってx,y,zで回転を表している
        characterRot *= Quaternion.Euler(0, xRot, 0);          //４元数をオイラー角を使ってx,y,zで回転を表している
                                                               //zは基本的に回転には使わない
        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);           

        cam.transform.localRotation = cameraRot;               //計算結果を反映
        transform.localRotation = characterRot;                //計算結果を反映

        UpdateCursorLock();

    }

    private void FixedUpdate()
    //毎フレームではなく、”物理演算の更新タイミング”で呼ばれる関数
    //このスクリプトで FixedUpdate() を使う理由は、「物理ベースの動き（Rigidbodyなしでも）」を安定して実行するため
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;     //Horixontalとは前後移動（WS）を表す
        z = Input.GetAxisRaw("Vertical") * speed;       //Verticalとは左右移動（AD）を表す
        //transform.position += new Vector3(x,0,z);

        Vector3 forward = cam.transform.forward; 
        Vector3 right = cam.transform.right;

        forward.y = 0f;  // 上下成分を消す。つまり、ｗ、ｓキーでジャンプしなくなる
        right.y = 0f;

        forward.Normalize();        //スカラーを１にして、向きのみの成分にしている
        right.Normalize();          //スカラーを１にして、向きのみの成分にしている

        transform.position += forward * z + right * x;
                            //向き*移動量 + 向き * 移動量
    }

    public void UpdateCursorLock()                      //UpdateCursorLockという名前のメソッド
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cursorLock = false;                         //cursorLockとはただのbool型の変数
        }

        else if(Input.GetMouseButton(0))
        {
            cursorLock= true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;   //Lokedにする＝マウスカーソルを画面の中心に固定して非表示にしてくれる
        }

        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;     //Noneにする＝マウスカーソルを表示して自由に動かせるようになる
        }
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)       //渡された回転をX軸（上下方向）に制限する関数
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;                                     //クランプとは簡単に制限することができる便利機能
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;   //回転のX軸の角度をラジアン→度に変換    
                                                               //アークタンジェントで角度（ラジアン）を求める。
                                                               //tan(θ) = x のとき、θ = atan(x) になる
                                                               //つまり、「この値はどの角度のタンジェントか？」を求める
        angleX = Mathf.Clamp(angleX,minX,maxX);
        //"Mathf.Clamp"とは、angleXを(min,max) = (minX,minX)とする処理

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);        //Mathf.Deg2Rad      度からラジアン変換

        return q;       //X軸だけ制限された新しいクォータニオンを返す。
    }
}
    

