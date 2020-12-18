using UnityEngine;
using Cinemachine;

[CreateAssetMenu(menuName = "MyScriptable/Create PlayerMoveData2")]
public class PlayerMoveData2 : ScriptableObject
{
    // 移動可能パスをすべて設定
    //[NamedArrayAttribute(new string[] { "New Game", "Continue", "Options" })]
    public CinemachineSmoothPath[] movePath = null;

    [Header("移動可能な場所")]
    public MoveD[] moveDs = null;
}

[System.Serializable]
public class MoveD
{
    // 移動開始地点
    public float startPos = 0.0f;
    // 移動終了地点
    public float endPos = 0.0f;
    // 移動可能なパスの番号
    [Header("移動可能なパスの番号")]
    public int[] moveNum = null;

    // ジャンプが可能か
    bool isJumpPath;
}