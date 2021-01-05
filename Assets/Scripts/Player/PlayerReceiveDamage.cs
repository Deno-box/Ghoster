using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveDamage : MonoBehaviour
{
    // プレイヤーのステータスデータ
    private PlayerStatusData playerStatus = null;
    PlayerData playerData;

    // 被ダメージ状態での経過時間
    private float damageTimer;
    // 点滅時間
    private float brinkInterval = 0.15f;
    // 点滅状態か
    private bool isBlink = false;
    
    [SerializeField]
    private GameObject playerModel = null;
    private GameObject reciveDamageFX;
    [SerializeField]
    private GameObject damageCollider = null;
    private PlayerHitStop player;
    [SerializeField]
    private CameraShake shakeCamera = null;

    private void Start()
    {
        this.playerData = this.transform.GetComponentInParent<PlayerData>();
        this.playerStatus = playerData.PlayerStatus;
        this.reciveDamageFX = Resources.Load("Effect/Player/PlayerReceiveDamage") as GameObject;

        player = this.GetComponent<PlayerHitStop>();
    }

    // 初期化処理
    public void Initialize()
    {
        damageTimer = 0.0f;
        this.isBlink = true;
        GameDataManager.AddDecisionNum((int)GameDataManager.SCORE_TYPE.MISS);

        // ダメージ用のエフェクトを生成
        GameObject obj = Instantiate(reciveDamageFX, this.transform);
        StartCoroutine("BlinkRenderer");

        damageCollider.SetActive(false);
        
        player.SlowDown(playerStatus.cameraShakeTime, playerStatus.cameraShakeMagnitude);
        shakeCamera.Shake(playerStatus.cameraShakeTime, playerStatus.cameraShakeMagnitude);

        //playerData.AudioSource.PlayOneShot(playerStatus.receiveDamageSE);
    }

    // 実行処理
    public void Update()
    {
        if (this.isBlink == true)
        {
            this.damageTimer += Time.deltaTime;
            // 何かキーを押されるor一定時間経過で点滅付与状態解除
            if (Input.anyKey || this.damageTimer >= playerStatus.damageInvincibleTime)
                ReleaseBrink();
        }
    }

    // 終了処理
    public void ReleaseBrink()
    {
        playerModel.SetActive(true);
        damageCollider.SetActive(true);
        this.isBlink = false;
    }

    // TODO :: 演出用の処理で状態変更を行っているので変更する
    IEnumerator BlinkRenderer()
    {
        while (this.isBlink)
        {
            playerModel.SetActive(!playerModel.activeSelf);
            yield return new WaitForSeconds(this.brinkInterval);
        }
    }
}