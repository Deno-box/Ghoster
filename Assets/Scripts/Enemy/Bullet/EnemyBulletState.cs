using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletState : BulletState
{
    //// パリィ用エフェクト
    //private GameObject parryFX = null;

    // 生存用カウンター
    private float counter = 0;
    // 一定時間経過すると削除されるカウンター
    private float destroyCounterMax = 20.0f;
    // ダメージ用エフェクト
    private GameObject damageFX;
    // 生成演出
    float timeScale = 0.0f;

    [SerializeField]
    private GameObject satelliteObj = null;

    private void Start()
    {
        this.damageFX = Resources.Load("Effect/Enemy/Bullet/BossBulletCollision") as GameObject;
    }

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
        if (_other.tag == "PlayerBody")
            Destroy(this.gameObject);
    }

    public void ChangeParryBulletState()
    {
        state = EnemyBulletStateController.BulletStateEnum.Parry;

        if (satelliteObj)
            Destroy(this.satelliteObj);
    }
    public void DestroyBullet()
    {
        Instantiate(damageFX, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
