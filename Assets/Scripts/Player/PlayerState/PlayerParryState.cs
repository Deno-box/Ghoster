using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    // プレイヤーのステータスデータ
    private PlayerStatusData playerStatus = null;
    PlayerData playerData;

    // パリィ判定を行うオブジェクト
    [SerializeField]
    private GameObject parryObj = null;
    [SerializeField]
    private GameObject damageObj = null;
    private Vector3 parryObjOffset = new Vector3(0.0f,0.8f,1.0f);

    private PlayerHitStop player = null;
    [SerializeField]
    private CameraShake shakeCamera = null;


    // パリィを発生させてから実際に敵が当たるまでの時間
    private float parryJudgeTime = 0.0f;
    // パリィ成功時のエフェクト
    private GameObject parrysuccessFx;

    //[SerializeField]
    //private GameObject playerModel = null;
    private bool isInputMoveButton = false;

    private float beforeStick = 0.0f;
    private float beforeCross = 0.0f;

    private void Start()
    {
        playerData = this.GetComponent<PlayerData>();
        this.playerStatus = playerData.PlayerStatus;
        this.parrysuccessFx = Resources.Load("Effect/Player/ParrySuccess") as GameObject;

        //this.parryObj = Instantiate(Resources.Load("Prefabs/Player/PlayerParryJudgement") as GameObject, this.transform);
        parryObj.SetActive(false);

        //this.parryObj.transform.localPosition = parryObjOffset;

        player = this.GetComponent<PlayerHitStop>();

    }

    // 初期化処理
    public override void Initialize()
    {
        damageObj.SetActive(false);

        parryJudgeTime = 0.0f;
        this.state = PlayerStateController.PlayerStateEnum.Parry;

        PlayerData.ParryDirection parryDir = this.playerData.ParryDir;
        // はじいた方向に応じてアニメーションを変更
        switch (parryDir)
        {
            case PlayerData.ParryDirection.Right:
                this.GetComponent<PlayerData>().GetComponent<Animator>().Play("Right_Attack");
                break;

            case PlayerData.ParryDirection.Left:
                this.GetComponent<PlayerData>().GetComponent<Animator>().Play("Left_Attack");
                break;

            default:break;
        }


        // パリィ判定用オブジェクトをアクティブにする
        parryObj.SetActive(true);
        // 判定用のタイマーをリセット
        parryJudgeTime = 0.0f;

        isInputMoveButton = false;



        playerData.AudioSource.PlayOneShot(playerStatus.spinSE);
    }

    // 実行処理
    public override void Execute()
    {
        // パリィを発生していなかったら発生させる
        ParryAction();

        // パリィを行っていたら判定用タイマーを増加
        parryJudgeTime += Time.deltaTime;

        float stickHori = Input.GetAxisRaw("Horizontal");
        float crossHori = Input.GetAxisRaw("CrossHorizontal");

        if (Input.GetKeyDown(KeyCode.LeftArrow) || stickHori < 0 && this.beforeStick == 0 || crossHori < 0 && this.beforeCross == 0)
        {
            // 左入力キーを設定
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMovePath.MoveDir.Left;
            isInputMoveButton = true;
        }
        // Dキーで左のパスに移動
        else
        if (Input.GetKeyDown(KeyCode.RightArrow) || stickHori > 0 && this.beforeStick == 0 || crossHori > 0 && this.beforeCross == 0)
        {
            // 右入力キーを設定
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMovePath.MoveDir.Right;
            isInputMoveButton = true;
        }

        beforeStick = stickHori;
        beforeCross = crossHori;
    }

    // 移動実行処理
    public override void ExecuteMove()
    {
    }


    // 終了処理
    public override void Exit()
    {
        // 一定時間経過後パリィ判定用オブジェクトを非アクティブにする
        parryObj.SetActive(false);
        damageObj.SetActive(true);
    }


    // パリィを発生させる
    private void ParryAction()
    {
        // 生成してから一定時間経過していたらアイドル状態に遷移
        if (this.playerStatus.parryActiveTime <= parryJudgeTime)
        {
            if (isInputMoveButton)
                this.state = PlayerStateController.PlayerStateEnum.MoveLR;
            else
                this.state = PlayerStateController.PlayerStateEnum.Idle;
        }
    }

    // パリィ判定用オブジェクトに衝突したら
    public void ParryJudge()
    {
        // アイドル状態に遷移
        GameObject obj = Instantiate(parrysuccessFx, this.transform);
        obj.transform.localPosition = new Vector3(0.0f, 0.0f, 4.0f);
        ParryJudgement();


        player.SlowDown(playerStatus.cameraShakeTime, playerStatus.cameraShakeMagnitude);
        shakeCamera.Shake(playerStatus.cameraShakeTime, playerStatus.cameraShakeMagnitude);

        playerData.AudioSource.PlayOneShot(playerStatus.parrySE);
    }

    // great,good判定を取る
    private void ParryJudgement()
    {
        if (parryJudgeTime <= this.playerStatus.goodJudgeDistance)
            GameDataManager.AddDecisionNum((int)GameDataManager.SCORE_TYPE.GOOD);
        else
        if (parryJudgeTime <= this.playerStatus.greatJudgeDistance)
            GameDataManager.AddDecisionNum((int)GameDataManager.SCORE_TYPE.GREAT);
    }
}