using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    void Start()
    {
        transform.position = new Vector3(0, 1, 20);
    }

    public void EnemyGenerate()
    {
        int xEnemyPos = Random.Range(-20, 21);  //-20以上21未満
        //変数"Xenemypos"の値をRandomクラスの、Rangeメソッドを使ってランダムに決めている

        int zPnemyPos = Random.Range(1, 21);    //1以上21未満
        //変数"Yenemypos"の値をRandomクラスの、Rangeメソッドを使ってランダムに決めている

        transform.position = new Vector3(xEnemyPos, 1, zPnemyPos);
        //敵をランダムな場所に一回配置
    }
}