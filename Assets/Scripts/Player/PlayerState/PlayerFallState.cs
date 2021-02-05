using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Common;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class PlayerFallState : PlayerState
{
    private Rigidbody rb;
    int stageNum;
    private GameObject damageFX;

    //bool型の変数を作る
    private static bool clearflag = true;

    //boolのプロパティゲッターを作る
    public static bool ClearFlag
    {
        get { return clearflag; }
    }

    private void Start()
    {
        this.damageFX = Resources.Load("Effect/Enemy/Bullet/BossBulletCollision") as GameObject;

       
    }

    // 初期化処理
    public override void Initialize()
    {
        // 移動を無効化
        this.GetComponent<CinemachineDollyCart>().enabled = false;
        // カメラの追尾を無効化
        Camera.main.GetComponent<PlayerFollowCamera>().enabled = false;

        stageNum = int.Parse(Regex.Replace(SceneManager.GetActiveScene().name, @"[^0-9]", ""));

        switch (stageNum)
        {
            case 1:  InitGroundStage(); break;
            case 2:  InitAirStage();    break;
            case 3:  InitAirStage();    break;
            default: break;
        }

        GameObject.Find("Director").GetComponent<PlaySceneController>().ChangeState(PlaySceneController.State.ExitScene);
        //フラグをフォルスに変える
         clearflag = false;


}

// 実行処理
public override void Execute()
    {
        switch (stageNum)
        {
            case 1: ExecuteGroundStage(); break;
            case 2: ExecuteAirStage();    break;
            case 3: ExecuteAirStage();    break;
            default: break;
        }
    }

    // 移動実行処理
    public override void ExecuteMove()
    {
    }

    // 終了処理
    public override void Exit()
    {
    }

    // 地上のステージでの落下演出
    private void InitGroundStage()
    {
        this.GetComponent<PlayerData>().GetComponent<Animator>().Play("FallGround");
        this.GetComponent<PlayerData>().GetComponent<Animator>().speed = 0.15f;

        GameObject obj = Instantiate(damageFX, this.transform);

    }
    // 空中のステージでの落下演出
    private void InitAirStage()
    {
        this.GetComponent<PlayerData>().GetComponent<Animator>().Play("FallAir");
        this.GetComponent<PlayerData>().GetComponent<Animator>().speed = 0.5f;

        // 重力を追加
        this.gameObject.AddComponent<Rigidbody>();
        this.rb = this.GetComponent<Rigidbody>();

    }

    private void ExecuteGroundStage()
    {
        this.transform.Rotate(new Vector3(-0.6f, 0.0f, 0.0f));
    }
    private void ExecuteAirStage()
    {
        Vector3 torqueVec = this.transform.forward * 13.0f;
        this.rb.AddForce(torqueVec, ForceMode.Impulse);
    }
}
