using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyScriptable/Create PlayerStatusData")]
public class PlayerStatusData : ScriptableObject
{
    // 左右の移動時間
    public float moveTime;
    // パリィの有効時間
    public float parryActiveTime;
    // Good判定の距離
    public float goodJudgeDistance;
    // Great判定の距離
    public float greatJudgeDistance;
    // ダメージを受けた時の無敵時間
    public float damageInvincibleTime;
    // 落下判定を取るパスの長さの割合
    [Range(0.0f,1.0f)]
    public float fallJudgeRate;
    // 警告を出すパスの長さの割合
    public float alertDistance;
}
