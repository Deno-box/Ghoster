using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    // 実行しているステート
    protected PlayerStateController.PlayerStateEnum state;
    public PlayerStateController.PlayerStateEnum State { get { return state; } }

    //// 次のステート
    //protected PlayerStateController.PlayerStateEnum nextState;
    //public PlayerStateController.PlayerStateEnum NextState { get { return nextState; } }

    // 初期化処理
    public abstract void Initialize();

    // 実行処理
    public abstract void Execute();

    // 終了処理
    public abstract void Exit();
}
