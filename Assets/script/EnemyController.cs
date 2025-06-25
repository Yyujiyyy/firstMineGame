using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //[SerializeField] private int _attackPower = 10;   先輩

    // Start is called before the first frame update
    void Start()
    {
        //EnemyGenerate();

        transform.position = new Vector3(0, 1, 20);

        //Debug.Log("生成");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
            //EnemyGenerate();
    }

    /* private void OnCollisionEnter(Collision collision)
     //Rigidbody を持つオブジェクトが他の Collider にぶつかった瞬間に実行される
     {
         if (collision.gameObject.tag == "enemy" && collision.gameObject != this.gameObject)//修正する！！
         {
             Destroy(collision.gameObject);

             int Xenemypos = Random.Range(1, 21);
             int Zenemypos = Random.Range(1, 21);    //位置を取得

             transform.position = new Vector3(Xenemypos, 1, Zenemypos);      //配置
         }
     }*/

    public void EnemyGenerate()
    {
        int xEnemyPos = Random.Range(-20, 21);
        //変数"Xenemypos"の値をRandomクラスの、Rangeメソッドを使ってランダムに決めている

        int zPnemyPos = Random.Range(1, 21);
        //変数"Yenemypos"の値をRandomクラスの、Rangeメソッドを使ってランダムに決めている

        transform.position = new Vector3(xEnemyPos, 1, zPnemyPos);
        //敵をランダムな場所に一回配置

        //Debug.Log("random生成");


    }
}


//カメラをcapsulに付け、マウスの動きで視点移動をさせる
