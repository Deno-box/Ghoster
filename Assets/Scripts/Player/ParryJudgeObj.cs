using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryJudgeObj : MonoBehaviour
{
    // 自身の親オブジェクト
    private PlayerParryState player = null;
    private PlayerData playerData;

    private void Start()
    {
        player = this.transform.parent.GetComponent<PlayerParryState>();
        this.playerData = this.GetComponentInParent<PlayerData>();
    }

    // 敵に衝突したらプレイヤーのパリィアクションに報告を行う
    void OnTriggerEnter(Collider _other)
    {
        // プレイヤーに衝突の報告を行う
        if (_other.tag == "EnemyBullet")
        {
            PlayerData.ParryDirection bulletDir = _other.GetComponent<BulletStatus>().Dir;
            PlayerData.ParryDirection parryDir = this.playerData.ParryDir;
            if (bulletDir == PlayerData.ParryDirection.Allways ||
                (bulletDir == PlayerData.ParryDirection.Right && parryDir <= bulletDir) ||
                (bulletDir == PlayerData.ParryDirection.Left && parryDir >= bulletDir))
            {
                player.ParryJudge();
                _other.GetComponent<EnemyBulletState>().ChangeParryBulletState();
            }
            else
            {
                //player.ReceiveDamage();
                this.GetComponentInParent<PlayerReceiveDamage>().Initialize();
                _other.GetComponent<EnemyBulletState>().DestroyBullet();
            }
        }
    }
}
