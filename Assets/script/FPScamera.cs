using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FPScamera : MonoBehaviour
{
    float x, z;
    float speed = 0.1f;

    public GameObject cam;          //解説
    Quaternion cameraRot, characterRot;     //解説
    [Range(0.0001f, 40.000f)][SerializeField]float Sensitivity = 1f;
    bool cursorLock = true;         //解説
    float minX = -90, maxX = 90f;

    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * Sensitivity;
        float yRot = Input.GetAxis("Mouse Y") * Sensitivity;
        //マウスの移動量　       ×　   感度

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);     //解説
        characterRot *= Quaternion.Euler(0, xRot, 0);   //解説

        //Updateの中で作成した関数を呼ぶ
        cameraRot = ClampRotation(cameraRot);           //解説

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;


        UpdateCursorLock();

    }

    private void FixedUpdate()                          //解説
    //毎フレームではなく、”物理演算の更新タイミング”で呼ばれる関数
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;     //解説
        z = Input.GetAxisRaw("Vertical") * speed;       //解説
        //transform.position += new Vector3(x,0,z);

        Vector3 forward = cam.transform.forward; 
        Vector3 right = cam.transform.right;

        forward.y = 0f;  // 上下成分を消す。つまり、ｗ、ｓキーでジャンプしなくなる
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        transform.position += forward * z + right * x;
        //カメラの向いている方向を基準にしてキャラクターを移動　バカムズイ
    }

    public void UpdateCursorLock()                      //カーソルロックとは
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;                         //true,falseについて
        }

        else if(Input.GetMouseButton(0))
        {
            cursorLock= true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //角度制限関数の作成
    public Quaternion ClampRotation(Quaternion q)       //渡された回転をX軸（上下方向）に制限する関数
    {
        //q = x,y,z,w (x,y,zはベクトル（量と向き）：wはスカラー（座標とは無関係の量）)

        q.x /= q.w;                                     //クランプとは
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;   //回転のX軸の角度をラジアン→度に変換    解説

        angleX = Mathf.Clamp(angleX,minX,maxX);
        //"Mathf.Clamp"とは、angleXを(min,max) = (minX,minX)とする処理

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
    

