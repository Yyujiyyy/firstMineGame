using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(0, 1, 20);
    }

    public void EnemyGenerate()
    {
        int xEnemyPos = Random.Range(-20, 21);  //-20�ȏ�21����
        //�ϐ�"Xenemypos"�̒l��Random�N���X�́ARange���\�b�h���g���ă����_���Ɍ��߂Ă���

        int zPnemyPos = Random.Range(1, 21);    //1�ȏ�21����
        //�ϐ�"Yenemypos"�̒l��Random�N���X�́ARange���\�b�h���g���ă����_���Ɍ��߂Ă���

        transform.position = new Vector3(xEnemyPos, 1, zPnemyPos);
        //�G�������_���ȏꏊ�Ɉ��z�u
    }
}