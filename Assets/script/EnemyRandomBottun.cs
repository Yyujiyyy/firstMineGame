using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomBottun : MonoBehaviour
{
    [SerializeField]private EnemyController enemyController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            enemyController = GetComponent<EnemyController>();
    }
}
