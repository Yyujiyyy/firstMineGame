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

    // Start is called before the first frame update
    void Start()
    {
        cameraRot = cam.transform.localRotation;
        characterRot = transform.localRotation;

        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            Sensitivity = PlayerPrefs.GetFloat("Sensitivity");      //Sensitivity�ϐ������ʂ̐��l�ɂ���
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            Sensitivity = PlayerPrefs.GetFloat("Sensitivity"); // ���t���[���擾
        }

        float xRot = Input.GetAxisRaw("Mouse X") * Sensitivity;
        float yRot = Input.GetAxisRaw("Mouse Y") * Sensitivity;
        //�}�E�X�̈ړ��ʁ@       �~�@   ���x

        cameraRot *= Quaternion.Euler(-yRot, 0, 0);            //�S�������I�C���[�p���g����x,y,z�ŉ�]��\���Ă���
        characterRot *= Quaternion.Euler(0, xRot, 0);          //�S�������I�C���[�p���g����x,y,z�ŉ�]��\���Ă���
                                                               //z�͊�{�I�ɉ�]�ɂ͎g��Ȃ�
        //Update�̒��ō쐬�����֐����Ă�
        cameraRot = ClampRotation(cameraRot);           

        cam.transform.localRotation = cameraRot;               //�v�Z���ʂ𔽉f
        transform.localRotation = characterRot;                //�v�Z���ʂ𔽉f

        UpdateCursorLock();

    }

    private void FixedUpdate()
    //���t���[���ł͂Ȃ��A�h�������Z�̍X�V�^�C�~���O�h�ŌĂ΂��֐�
    //���̃X�N���v�g�� FixedUpdate() ���g�����R�́A�u�����x�[�X�̓����iRigidbody�Ȃ��ł��j�v�����肵�Ď��s���邽��
    {
        x = 0;
        z = 0;

        x = Input.GetAxisRaw("Horizontal") * speed;     //Horixontal�Ƃ͑O��ړ��iWS�j��\��
        z = Input.GetAxisRaw("Vertical") * speed;       //Vertical�Ƃ͍��E�ړ��iAD�j��\��
        //transform.position += new Vector3(x,0,z);

        Vector3 forward = cam.transform.forward; 
        Vector3 right = cam.transform.right;

        forward.y = 0f;  // �㉺�����������B�܂�A���A���L�[�ŃW�����v���Ȃ��Ȃ�
        right.y = 0f;

        forward.Normalize();        //�X�J���[���P�ɂ��āA�����݂̂̐����ɂ��Ă���
        right.Normalize();          //�X�J���[���P�ɂ��āA�����݂̂̐����ɂ��Ă���

        transform.position += forward * z + right * x;
                            //����*�ړ��� + ���� * �ړ���
    }

    public void UpdateCursorLock()                      //UpdateCursorLock�Ƃ������O�̃��\�b�h
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLock = false;                         //cursorLock�Ƃ͂�����bool�^�̕ϐ�
        }

        else if(Input.GetMouseButton(0))
        {
            cursorLock= true;
        }


        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;   //Loked�ɂ��遁�}�E�X�J�[�\������ʂ̒��S�ɌŒ肵�Ĕ�\���ɂ��Ă����
        }

        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;     //None�ɂ��遁�}�E�X�J�[�\����\�����Ď��R�ɓ�������悤�ɂȂ�
        }
    }

    //�p�x�����֐��̍쐬
    public Quaternion ClampRotation(Quaternion q)       //�n���ꂽ��]��X���i�㉺�����j�ɐ�������֐�
    {
        //q = x,y,z,w (x,y,z�̓x�N�g���i�ʂƌ����j�Fw�̓X�J���[�i���W�Ƃ͖��֌W�̗ʁj)

        q.x /= q.w;                                     //�N�����v�Ƃ͊ȒP�ɐ������邱�Ƃ��ł���֗��@�\
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1f;

        float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;   //��]��X���̊p�x�����W�A�����x�ɕϊ�    
                                                               //�A�[�N�^���W�F���g�Ŋp�x�i���W�A���j�����߂�B
                                                               //tan(��) = x �̂Ƃ��A�� = atan(x) �ɂȂ�
                                                               //�܂�A�u���̒l�͂ǂ̊p�x�̃^���W�F���g���H�v�����߂�
        angleX = Mathf.Clamp(angleX,minX,maxX);
        //"Mathf.Clamp"�Ƃ́AangleX��(min,max) = (minX,minX)�Ƃ��鏈��

        q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);        //Mathf.Deg2Rad      �x���烉�W�A���ϊ�

        return q;       //X�������������ꂽ�V�����N�H�[�^�j�I����Ԃ��B
    }
}
    

