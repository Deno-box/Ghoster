using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    // プレイヤーのステート
    public enum PlayerStateEnum
    {
        Idle,           // アイドル状態
        MoveLR,         // 左右移動状態
        Parry,          // パリィ状態
        ReceiveDamage,  // 非ダメージ状態
        Fall,           // 落下状態
        Comeback,       // 復帰状態
        None            // 初期状態
    }

    // 現在実行しているステート
    private PlayerState activeState = null;
    private PlayerStateEnum lastActiveStateEum = PlayerStateEnum.None;
    private PlayerState[] stateList = new PlayerState[(int)PlayerStateEnum.None];

    // Start is called before the first frame update
    void Start()
    {
        // 全てのステートを登録
        this.gameObject.AddComponent<PlayerIdleState>();
        this.gameObject.AddComponent<PlayerMoveLRState>();
        this.gameObject.AddComponent<PlayerParryState>();
        this.gameObject.AddComponent<PlayerReceiveDamageState>();
        this.gameObject.AddComponent<PlayerFallState>();
        stateList[(int)PlayerStateEnum.Idle]    = this.GetComponent<PlayerIdleState>();
        stateList[(int)PlayerStateEnum.MoveLR]  = this.GetComponent<PlayerMoveLRState>();
        stateList[(int)PlayerStateEnum.Parry]   = this.GetComponent<PlayerParryState>();
        stateList[(int)PlayerStateEnum.ReceiveDamage]   = this.GetComponent<PlayerReceiveDamageState>();
        stateList[(int)PlayerStateEnum.Fall]   = this.GetComponent<PlayerFallState>();

        // アクティブステートをアイドル状態に初期化
        activeState = stateList[(int)PlayerStateEnum.Idle];
        activeState.Initialize();
        lastActiveStateEum = activeState.State;
    }

    // Update is called once per frame
    void Update()
    {
        activeState.Execute();

        // ステートが変更されていたら
        if (lastActiveStateEum != activeState.State)
        {
            activeState.Exit();
            activeState = stateList[(int)activeState.State];
            activeState.Initialize();
            lastActiveStateEum = activeState.State;
        }

    }
}
