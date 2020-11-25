using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 何かの範囲に入ったら
    private void OnTriggerEnter(Collider other)
    {
        // パリィに衝突したなら自身を削除
        if (other.tag == "PlayerParry")
            EnemyDestroy();
    }
}
