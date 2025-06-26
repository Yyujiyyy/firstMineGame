using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private EnemyController _random;
    public ParticleSystem particle;   // Hierarchy���ParticleSystem���w��
    [SerializeField] public Transform enemy;
     Transform _tr;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("enemy").transform;

        _random = GameObject.FindObjectOfType<EnemyController>();
        //����ŃV�[�����̍ŏ��Ɍ������� EnemyController �� _random �ɃZ�b�g�ł���B

        _tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X�����N���b�N�����Ƃ�
        if (Input.GetMouseButtonDown(0))
        {
            //�V����Ray�����B
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                            �@//Ray�̔��˒n�_               ,����

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
            {
                                                    

                if (hitInfo.collider.CompareTag("enemy"))
                {
                    //particle.transform.position = hitInfo.point;       //particle�̈ʒu�𓖂������ʒu�ɂ���i���[�v��ɂ��Ă��Ȃ��j
                    //instantiate���邩��K�v�Ȃ���
                    Instantiate(particle,hitInfo.point, Quaternion.identity);

                    if (_random != null)                    //null�`�F�b�N      ���
                    {
                        _random.EnemyGenerate();            //_random �ɓ����Ă���I�u�W�F�N�g�ɑ΂��� EnemyGenerate() ���Ăяo��
                    }
                }
            }
        }
    }
}