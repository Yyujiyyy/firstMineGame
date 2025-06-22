using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private EnemyController _enemyPrefab;
    //[SerializeField] private EnemyController _enemy;
    [SerializeField] private EnemyController _random;
    public ParticleSystem particle;   // Hierarchy���ParticleSystem���w��
    [SerializeField] public Transform enemy;
     Transform _tr;

    //public EnemyController enemycontroller;



    // Start is called before the first frame update
    void Start()
    {
        //_enemy = Instantiate(_enemyPrefab, transform.position, _enemyPrefab.transform.rotation);

        enemy = GameObject.FindWithTag("enemy").transform;

        _random = GameObject.FindObjectOfType<EnemyController>();
        //����ŃV�[�����̍ŏ��Ɍ������� EnemyController �� _random �ɃZ�b�g�ł��܂��B

        _tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    _enemy = Instantiate(_enemyPrefab, transform.position, _enemyPrefab.transform.rotation);
        //}                              ��y

        //�}�E�X�����N���b�N�����Ƃ�
        if (Input.GetMouseButtonDown(0))
        {
            
            //_enemy.EnemyGenerate();  �@��y


            //�V����Ray�����B
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //Ray�̔��˒n�_               ,����

            //Ray�̉��� �@Ray�̌��_�@Ray�̕����@�@Ray�̐F         *�������Q�[���V�[���ɂ͕`����Ȃ�
            //Debug.DrawLine(ray.origin, ray.direction * 100f, Color.yellow);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
            {
                //Debug.Log("���������I" + hitInfo.point + hitInfo.collider.name);
                //�h���������I�h�@�{�@���������ꏊ�@�{�@�����������̖̂��O

                //Destroy(hitInfo.collider.gameObject);
                //�I�u�W�F�N�g�{�̂��ƍ폜"gameObject"���Ȃ���collider�݂̂̍폜�ƂȂ�A�{�͎̂c�葱����


                                                    //particle�����I�I�I

                if (hitInfo.collider.CompareTag("enemy"))
                {
                    particle.transform.position = hitInfo.collider.transform.position;       //particle�̈ʒu�𓖂������ʒu�ɂ���i���[�v��ɂ��Ă��Ȃ��j
                    particle.Play();

                    if (_random != null)                    //null�`�F�b�N      ���
                    {
                        _random.EnemyGenerate();
                    }
                    else
                    {
                        //Debug.LogWarning("_random��null�ł��BEnemyController��������܂���B");
                    }
                }
            }
        }
    }
}
//score,timer,ui,target,head    e.t.c.