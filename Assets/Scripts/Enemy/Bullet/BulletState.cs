using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletState : MonoBehaviour
{
    // 実行したいステート
    protected EnemyBulletStateController.BulletStateEnum state;
    public EnemyBulletStateController.BulletStateEnum State { get { return state; } }

    // 初期化処理
    public virtual void StateInitialize() { }
    // 更新処理
    public virtual void Execute(){ }
    // OnTrigger時の処理
    public virtual void StateOnTriggerEnter(Collider _other) { }
}