using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField]
    float bossHitPoint = 200.0f;
    [SerializeField]
    float damage = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(bossHitPoint <= 0.0f)
        {
            EnemyDestroy();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        bossHitPoint -= damage;
    }
}
