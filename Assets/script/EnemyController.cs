using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] private int _attackPower = 10;   ��y

    // Start is called before the first frame update
    void Start()
    {
        //EnemyGenerate();

        transform.position = new Vector3(0, 1, 20);

        //Debug.Log("����");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
            //EnemyGenerate();
    }

    /* private void OnCollisionEnter(Collision collision)
     //Rigidbody �����I�u�W�F�N�g������ Collider �ɂԂ������u�ԂɎ��s�����
     {
         if (collision.gameObject.tag == "enemy" && collision.gameObject != this.gameObject)//�C������I�I
         {
             Destroy(collision.gameObject);

             int Xenemypos = Random.Range(1, 21);
             int Zenemypos = Random.Range(1, 21);    //�ʒu���擾

             transform.position = new Vector3(Xenemypos, 1, Zenemypos);      //�z�u
         }
     }*/

    public void EnemyGenerate()
    {
        int xEnemyPos = Random.Range(-20, 21);
        //�ϐ�"Xenemypos"�̒l��Random�N���X�́ARange���\�b�h���g���ă����_���Ɍ��߂Ă���

        int zPnemyPos = Random.Range(1, 21);
        //�ϐ�"Yenemypos"�̒l��Random�N���X�́ARange���\�b�h���g���ă����_���Ɍ��߂Ă���

        transform.position = new Vector3(xEnemyPos, 1, zPnemyPos);
        //�G�������_���ȏꏊ�Ɉ��z�u

        //Debug.Log("random����");


    }
}


//�J������capsul�ɕt���A�}�E�X�̓����Ŏ��_�ړ���������
