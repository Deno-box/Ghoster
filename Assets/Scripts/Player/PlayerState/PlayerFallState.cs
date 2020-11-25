using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerFallState : PlayerState
{
    private Rigidbody rb;

    // 初期化処理
    public override void Initialize()
    {
        // 移動を無効化
        this.GetComponent<CinemachineDollyCart>().enabled = false;
        // カメラの追尾を無効化
        Camera.main.GetComponent<PlayerFollowCamera>().enabled = false;

        // 重力を追加
        this.gameObject.AddComponent<Rigidbody>();
        this.rb = this.GetComponent<Rigidbody>();

        Vector3 torqueVec = this.transform.forward * 40.0f;
        this.rb.AddForce(torqueVec,ForceMode.Impulse);

        Fadecontroller fadeController = GameObject.Find("FadeCanvas").GetComponent<Fadecontroller>();
        //fadeController.fadeOutStart(0, 0, 0, 0, "DemoResultScene");
    }

    // 実行処理
    public override void Execute()
    {
    }

    // 終了処理
    public override void Exit()
    {
    }
}
