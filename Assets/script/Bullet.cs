using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform origin;   // Ray�̊J�n�ʒu
    public float length = 2f; // Ray�̒���
    public float speed = 20f;
    public float lifeTime = 1f;

    private LineRenderer lineRenderer;
    private Vector3 startPos;
    private Vector3 direction;
    private bool isFiring = false;      //���ݒe�����˒����ǂ����̃t���O�B
    private float timer = 0f;

    void Start()
    {
        if(origin == null)
        {
            origin = Camera.main.transform;
        }

        // LineRenderer�ǉ�
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;     //�[�̑���
        lineRenderer.endWidth = 0.1f;       //�[�̑���
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); //����\�����邽�߂̃}�e���A���i�F�𔽉f�����邽�߂̐ݒ�j
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.positionCount = 2; // 2�_�Ő�     ����2�_�i�n�_�ƏI�_�j�ō\���B
        lineRenderer.enabled = false;   //�ŏ��͔�\��
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }

        if (isFiring)       //�e�����ݔ��˒��ł���΁A�ȉ��̏��������s�B
        {
            startPos += direction * speed * Time.deltaTime;     //startPos���v�Z       Time.deltaTime �Ŗ��b�ړ��ɕ␳�B
            //FPS�Ɋ֌W�Ȃ��A1�b�ԂŁuspeed�v���i�ށB  speed�݂̂��ƍ�FPS�ő����i�݂�����B��FPS���ƒx���Ȃ�B

            Vector3 endPos = startPos + direction * length;     //endPos���v�Z     �i�n�_����Ray�̒����Ԃ��j
            lineRenderer.SetPosition(0, startPos);              //�v�Z���ʂ𔽉f
            lineRenderer.SetPosition(1, endPos);                //�v�Z���ʂ𔽉f

            timer += Time.deltaTime;                            //�o�ߎ��Ԃ����Z�B  �O�̃t���[�����獡�̃t���[���܂łɂ�����������
            if (timer >= lifeTime)
            {
                lineRenderer.enabled = false;
                isFiring = false;   //����
            }
        }
    }

    void Fire()
    {
        startPos = origin.position;
        direction = origin.forward.normalized;
        isFiring = true;
        timer = 0f;                                 //�o�ߎ��Ԃ����Z�B
        lineRenderer.enabled = true;                //����\���J�n�B
        //setActive�͂ǂ̃^�C�~���O�ł��g����A�q�I�u�W�F�N�g���܂Ƃ߂Ė����A���Ă��܂�
    }
}
