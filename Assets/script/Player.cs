using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
    [SerializeField] private EnemyController _random;
    public ParticleSystem particle;   // Hierarchy���ParticleSystem���w��
    [SerializeField] public Transform enemy;
    Transform _tr;

    // UI�֘A�iInspector�Őݒ�j
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GraphicRaycaster uiRaycaster;
    [SerializeField] private EventSystem eventSystem;

    // Ray�Ńq�b�g����UI�Ώۂ��L�^�i�A�����ē����Ώۂ��������Ȃ��悤�ɂ���j
    private CheckBox currentRayTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindWithTag("enemy").transform;

        _random = GameObject.FindObjectOfType<EnemyController>();
        //����ŃV�[�����̍ŏ��Ɍ������� EnemyController �� _random �ɃZ�b�g�ł���B

        _tr = transform;

        // �J���������ݒ�Ȃ玩���Ŏ擾
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X�����N���b�N�����Ƃ�
        if (Input.GetMouseButtonDown(0))
        {
            // ================================
            // �ʏ��3D��Ԃւ�Ray����
            // ================================

            //�V����Ray�����B
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            //Ray�̔��˒n�_               ,����

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
            {
                if (hitInfo.collider.CompareTag("enemy"))
                {
                    //particle.transform.position = hitInfo.point;       //particle�̈ʒu�𓖂������ʒu�ɂ���i���[�v��ɂ��Ă��Ȃ��j
                    //instantiate���邩��K�v�Ȃ���
                    Instantiate(particle, hitInfo.point, Quaternion.identity);

                    if (_random != null)                    //null�`�F�b�N      ���
                    {
                        _random.EnemyGenerate();            //_random �ɓ����Ă���I�u�W�F�N�g�ɑ΂��� EnemyGenerate() ���Ăяo��
                    }
                }

                // ================================
                // ���C�� "CheckBox" �ɓ��������ꍇ UI �\��
                // ================================
                if (hitInfo.collider.CompareTag("CheckBox"))
                {
                    CheckBox target = hitInfo.collider.GetComponent<CheckBox>();
                    if (target != null && target != currentRayTarget)
                    {
                        if (currentRayTarget != null)
                            currentRayTarget.HideUI(); // �O�̃^�[�Q�b�g��UI���\��

                        target.ShowUI();               // ���̃^�[�Q�b�g��UI��\��
                        currentRayTarget = target;     // �L�^�X�V
                    }
                }
            }
            else
            {
                //// Ray�����ɂ�������Ȃ������ꍇ�AUI���B��
                //if (currentRayTarget != null)
                //{
                //    currentRayTarget.HideUI();
                //    currentRayTarget = null;
                //    Debug.Log("�Ȃɂ��Ȃ�");
                //}
            }
        }
    }
}