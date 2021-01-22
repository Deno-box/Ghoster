using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(EnemyBulletState))]
[RequireComponent(typeof(EnemyBulletParryState))]
public class EnemyBulletStateController : MonoBehaviour
{
    public enum BulletStateEnum
    {
        EnemyBullet,    // 敵の弾の状態
        Parry,          // はじかれた後の状態
        None            // 初期化
    }

    // 自身のステート
    private BulletStateEnum lastStateEnum = BulletStateEnum.EnemyBullet;
    public BulletStateEnum LastStateEnum { get { return lastStateEnum; } }
    private BulletState activeState;
    private BulletState[] stateList = new BulletState[2];

    // Start
    private void Start()
    {
        // ステートを初期化
        stateList[(int)BulletStateEnum.EnemyBullet] = GetComponent<EnemyBulletState>();
        stateList[(int)BulletStateEnum.Parry] = GetComponent<EnemyBulletParryState>();

        activeState = stateList[0];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 実行されるステートが変更されていたら変更を行う
        if (lastStateEnum != activeState.State)
        {
            lastStateEnum = activeState.State;
            activeState = stateList[(int)lastStateEnum];
            activeState.StateInitialize();
        }

        // 現在アクティブなステートを更新
        this.activeState.Execute();
    }

    private void OnTriggerEnter(Collider _other)
    {
        this.activeState.StateOnTriggerEnter(_other);
    }
}
