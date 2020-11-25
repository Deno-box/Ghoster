using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryJudgeObj : MonoBehaviour
{
    // 自身の親オブジェクト
    private PlayerParryState player = null;

    private void Start()
    {
        player = this.transform.parent.GetComponent<PlayerParryState>();
    }

    // 敵に衝突したらプレイヤーのパリィアクションに報告を行う
    void OnTriggerEnter(Collider _other)
    {
        // プレイヤーに衝突の報告を行う
        if (_other.tag == "EnemyBullet")
            player.ParryJudge();
    }
}
