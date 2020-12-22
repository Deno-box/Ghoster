using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerIdleState))]
[RequireComponent(typeof(PlayerMoveLRState))]
[RequireComponent(typeof(PlayerParryState))]
//[RequireComponent(typeof(PlayerReceiveDamageState))]
[RequireComponent(typeof(PlayerFallState))]
[RequireComponent(typeof(PlayerJumpState))]

public class PlayerStateController : MonoBehaviour
{
    // プレイヤーのステート
    public enum PlayerStateEnum
    {
        Idle,           // アイドル状態
        MoveLR,         // 左右移動状態
        Parry,          // パリィ状態
        Fall,           // 落下状態
        Jump,           // ジャンプ状態
        None            // 初期状態
    }

    // 現在実行しているステート
    private PlayerState activeState = null;
    private PlayerStateEnum lastActiveStateEum = PlayerStateEnum.None;
    private PlayerState[] stateList = new PlayerState[(int)PlayerStateEnum.None];

    public PlayerState ActiveState { get { return activeState; } }

    // Start is called before the first frame update
    void Awake()
    {
        // 全てのステートを登録
        stateList[(int)PlayerStateEnum.Idle]    = this.GetComponent<PlayerIdleState>();
        stateList[(int)PlayerStateEnum.MoveLR]  = this.GetComponent<PlayerMoveLRState>();
        stateList[(int)PlayerStateEnum.Parry]   = this.GetComponent<PlayerParryState>();
        stateList[(int)PlayerStateEnum.Fall]   = this.GetComponent<PlayerFallState>();
        stateList[(int)PlayerStateEnum.Jump]   = this.GetComponent<PlayerJumpState>();

        // アクティブステートをアイドル状態に初期化
        activeState = stateList[(int)PlayerStateEnum.Idle];
        activeState.Initialize();
        lastActiveStateEum = activeState.State;
    }

    // Update is called once per frame
    void Update()
    {
        this.activeState.Execute();

        // ステートが変更されていたら
        if (lastActiveStateEum != activeState.State)
        {
            activeState.Exit();
            activeState = stateList[(int)activeState.State];
            activeState.Initialize();
            lastActiveStateEum = activeState.State;
        }
    }

    private void FixedUpdate()
    {
        // 移動処理を実行
        this.activeState.ExecuteMove();
    }
}
