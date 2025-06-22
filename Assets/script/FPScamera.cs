using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class FPScamera : MonoBehaviour
{
    float x, z;
    float speed = 0.1f;

    public GameObject cam;          //���
    Quaternion cameraRot, characterRot;     //���
    [Range(0.0001f, 40.000f)][SerializeField]float Sensitivity = 1f;
    bool cursorLock = true;         //���
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
        //�}�E�X�̈ړ��ʁ@       �~�@   ���x

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);     //���
        characterRot *= Quaternion.Euler(0, xRot, 0);   //���

        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);           //���

        cam.transform.localRotation = cameraRot;
        transform.localRotation = characterRot;


        UpdateCursorLock();

    }

    private void FixedUpdate()                          //���
    //���t���[���ł͂Ȃ��A�h�������Z�̍X�V�^�C�~���O�h�ŌĂ΂��֐�
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;     //���
        z = Input.GetAxisRaw("Vertical") * speed;       //���
        //transform.position += new Vector3(x,0,z);

        Vector3 forward = cam.transform.forward; 
        Vector3 right = cam.transform.right;

        forward.y = 0f;  // �㉺�����������B�܂�A���A���L�[�ŃW�����v���Ȃ��Ȃ�
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        transform.position += forward * z + right * x;
        //�J�����̌����Ă����������ɂ��ăL�����N�^�[���ړ��@�o�J���Y�C
    }

    public void UpdateCursorLock()                      //�J�[�\�����b�N�Ƃ�
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;                         //true,false�ɂ���
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

    //�p�x�����֐��̍쐬
    public Quaternion ClampRotation(Quaternion q)       //�n���ꂽ��]��X���i�㉺�����j�ɐ�������֐�
    {
        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

        q.x /= q.w;                                     //�N�����v�Ƃ�
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;   //��]��X���̊p�x�����W�A�����x�ɕϊ�    ���

        angleX = Mathf.Clamp(angleX,minX,maxX);
        //"Mathf.Clamp"�Ƃ́AangleX��(min,max) = (minX,minX)�Ƃ��鏈��

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
    

