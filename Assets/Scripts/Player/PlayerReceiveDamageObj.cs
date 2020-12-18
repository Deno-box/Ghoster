using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveDamageObj : MonoBehaviour
{
    private PlayerStateController stateController;
    private PlayerIdleState idleState;
    private PlayerJumpState jumpState;

    private void Start()
    {
        idleState = this.transform.parent.parent.GetComponent<PlayerIdleState>();
        jumpState = this.transform.parent.parent.GetComponent<PlayerJumpState>();
        stateController = this.transform.parent.parent.GetComponent<PlayerStateController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnemyBullet")
        {
            // アイドル状態なら
            if (stateController.ActiveState.State == PlayerStateController.PlayerStateEnum.Idle)
                idleState.ReceiveDamage();
            // ジャンプ状態なら
            else
            if (stateController.ActiveState.State == PlayerStateController.PlayerStateEnum.Jump)
                jumpState.ReceiveDamage();
        }
    }
}
