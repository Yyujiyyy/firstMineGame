using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //[SerializeField] private EnemyController _enemyPrefab;
    //[SerializeField] private EnemyController _enemy;
    [SerializeField] private EnemyController _random;
    public ParticleSystem particle;   // Hierarchy上のParticleSystemを指定
    [SerializeField] public Transform enemy;
     Transform _tr;

    //public EnemyController enemycontroller;



    // Start is called before the first frame update
    void Start()
    {
        //_enemy = Instantiate(_enemyPrefab, transform.position, _enemyPrefab.transform.rotation);

        enemy = GameObject.FindWithTag("enemy").transform;

        _random = GameObject.FindObjectOfType<EnemyController>();
        //これでシーン内の最初に見つかった EnemyController を _random にセットできます。

        _tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    _enemy = Instantiate(_enemyPrefab, transform.position, _enemyPrefab.transform.rotation);
        //}                              先輩

        //マウスを左クリックしたとき
        if (Input.GetMouseButtonDown(0))
        {
            
            //_enemy.EnemyGenerate();  　先輩


            //新しいRayを作る。
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            //Rayの発射地点               ,方向

            //Rayの可視化 　Rayの原点　Rayの方向　　Rayの色         *ただしゲームシーンには描かれない
            //Debug.DrawLine(ray.origin, ray.direction * 100f, Color.yellow);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f))
            {
                //Debug.Log("当たった！" + hitInfo.point + hitInfo.collider.name);
                //”当たった！”　＋　当たった場所　＋　当たったものの名前

                //Destroy(hitInfo.collider.gameObject);
                //オブジェクト本体ごと削除"gameObject"がないとcolliderのみの削除となり、本体は残り続ける


                                                    //particle発動！！！

                if (hitInfo.collider.CompareTag("enemy"))
                {
                    particle.transform.position = hitInfo.collider.transform.position;       //particleの位置を当たった位置にする（ワープ先についてこない）
                    particle.Play();

                    if (_random != null)                    //nullチェック      解説
                    {
                        _random.EnemyGenerate();
                    }
                    else
                    {
                        //Debug.LogWarning("_randomがnullです。EnemyControllerが見つかりません。");
                    }
                }
            }
        }
    }
}
//score,timer,ui,target,head    e.t.c.