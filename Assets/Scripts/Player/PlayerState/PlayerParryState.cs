using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerState
{
    // プレイヤーのステータスデータ
    private PlayerStatusData playerStatus = null;

    // パリィ判定を行うオブジェクト
    [SerializeField]
    private GameObject parryObj = null;
    [SerializeField]
    private GameObject damageObj = null;
    private Vector3 parryObjOffset = new Vector3(0.0f,0.8f,1.0f);

    // パリィを発生させてから実際に敵が当たるまでの時間
    private float parryJudgeTime = 0.0f;
    // パリィ成功時のエフェクト
    private GameObject parrysuccessFx;


    // アニメーション用タイマー
    private float aniamtionTimer;
    private int rotDir = 1;

    //[SerializeField]
    //private GameObject playerModel = null;
    private bool isInputMoveButton = false;

    private float beforeStick = 0.0f;
    private float beforeCross = 0.0f;

    private void Start()
    {
        this.playerStatus = Resources.Load("ScriptableObjectDatas/Player/PlayerStatus") as PlayerStatusData;
        this.parrysuccessFx = Resources.Load("Effect/Player/ParrySuccess") as GameObject;

        //this.parryObj = Instantiate(Resources.Load("Prefabs/Player/PlayerParryJudgement") as GameObject, this.transform);
        parryObj.SetActive(false);

        //this.parryObj.transform.localPosition = parryObjOffset;

    }

    // 初期化処理
    public override void Initialize()
    {
        damageObj.SetActive(false);

        parryJudgeTime = 0.0f;
        this.state = PlayerStateController.PlayerStateEnum.Parry;

        // TODO: 変えておく
        if (this.rotDir == 1)
            this.rotDir = -1;
        else
            this.rotDir = 1;

        aniamtionTimer = 0.0f;

        // パリィ判定用オブジェクトをアクティブにする
        parryObj.SetActive(true);
        // 判定用のタイマーをリセット
        parryJudgeTime = 0.0f;

        isInputMoveButton = false;
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
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMoveData.MoveDir.Left;
            isInputMoveButton = true;
        }
        // Dキーで左のパスに移動
        else
        if (Input.GetKeyDown(KeyCode.RightArrow) || stickHori > 0 && this.beforeStick == 0 || crossHori > 0 && this.beforeCross == 0)
        {
            // 右入力キーを設定
            this.GetComponent<PlayerMoveLRState>().moveDir = PlayerMoveData.MoveDir.Right;
            isInputMoveButton = true;
        }

        PlayerRotation();

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

        //GameObject obj =  this.transform.GetChild(3).gameObject;
        //obj.transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, 0.0f);
    }


    // パリィを発生させるコルーチン
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

    // 回転させる
    void PlayerRotation()
    {
        float rate = 360.0f / this.playerStatus.parryActiveTime;
        aniamtionTimer += Time.deltaTime;
        //playerModel.transform.localRotation = Quaternion.Euler(-90.0f, 0.0f, aniamtionTimer * rate * rotDir);
    }
}