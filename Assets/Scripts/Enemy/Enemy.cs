using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 死亡時のエフェクト
    [SerializeField]
    protected GameObject destroyFX = null;

    // エフェクトを出して自身を削除
    protected void EnemyDestroy()
    {
        // エフェクトを生成
        // Instantiate(destroyFX, this.transform.position, Quaternion.identity);
        // 自身をｓ削除
        Destroy(this.gameObject);
    }
}
