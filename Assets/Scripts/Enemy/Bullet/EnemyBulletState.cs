using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletState : BulletState
{
    //// パリィ用エフェクト
    //private GameObject parryFX = null;
    //// ダメージ用エフェクト
    //private GameObject damageFX = null;

    // 生存用カウンター
    private float counter = 0;
    // 一定時間経過すると削除されるカウンター
    private float destroyCounterMax = 5.0f;

    // 初期化処理
    public override void StateInitialize()
    {
        state = EnemyBulletStateController.BulletStateEnum.EnemyBullet;
    }

    // 更新処理
    public override void Execute()
    {
        // 一定時間経過後、自身を削除する
        counter += Time.deltaTime;
        if (counter >= destroyCounterMax)
            Destroy(this.gameObject);
    }
    
    // OnTrigger時の処理
    public override void StateOnTriggerEnter(Collider _other)
    {
        // 敵の弾の状態でプレイヤーのパリィ範囲内に入ったらはじかれた後の状態に遷移
        if (_other.tag == "PlayerParry")
        {
            //Instantiate(damageFX, this.transform.position, Quaternion.identity);
            state = EnemyBulletStateController.BulletStateEnum.Parry;
        }
        else if(_other.tag == "PlayerBody")
        {
            //Instantiate(damageFX, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

}
