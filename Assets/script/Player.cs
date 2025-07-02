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
            // UI�{�^���ɑ΂���Ray����
            // ================================

            // PointerEventData�𐶐����A��ʒ��S���W���Z�b�g�i�܂���Input.mousePosition�ł�OK�j
            PointerEventData pointerData = new PointerEventData(eventSystem);
            pointerData.position = new Vector2(Screen.width / 2, Screen.height / 2);  // ��ʒ�������Ray���o��

            List<RaycastResult> uiResults = new List<RaycastResult>();
            uiRaycaster.Raycast(pointerData, uiResults);

            foreach (RaycastResult result in uiResults)
            {
                Button btn = result.gameObject.GetComponent<Button>();
                if (btn != null)
                {
                    btn.onClick.Invoke(); // �{�^���̃N���b�N�C�x���g�����s
                    return; // �{�^�����N���b�N���ꂽ�ꍇ�A�ȍ~�̏����͍s��Ȃ�
                }
            }

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
            }
        }
    }
}