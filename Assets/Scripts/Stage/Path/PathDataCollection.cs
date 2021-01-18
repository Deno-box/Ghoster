using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// 移動可能パスのデータを収集
public class PathDataCollection : MonoBehaviour
{
    // プレイヤーの移動可能データ
    public List<PlayerMoveData> playerMoveDatas = new List<PlayerMoveData>();

    public GameObject rootStagePath = null;

    // 移動元のmin,max,名前 移動先のmin,max
    void Awake()
    {
        int count = 0;
        // 分岐点を全て取得
        StageBranchPoint[] stageBranchPointList = rootStagePath.transform.GetComponentsInChildren<StageBranchPoint>();
        foreach (StageBranchPoint branchPT in stageBranchPointList)
        {
            // 移動パスデータを取得
            playerMoveDatas.Add(CollectPlayerData(branchPT, count));
            // 復帰パスデータを取得
            foreach (PathData pathData in branchPT.PathList)
                playerMoveDatas.Add(ComeBackPlayerData(branchPT, pathData));
            count++;
        }
    }
    // 移動パスのデータを取得
    private PlayerMoveData CollectPlayerData(StageBranchPoint _branchPT,int _count)
    {
        PlayerMoveData retData = new PlayerMoveData();
        // 移動元の情報を設定
        _branchPT.BeforeBranchPath.name = "Path"+ _count.ToString();
        retData.NowPath   = _branchPT.BeforeBranchPath;
        retData.nowPosMin = _branchPT.gameObject.GetComponent<CinemachineDollyCart>().m_Position;
        retData.nowPosMax = _branchPT.EndPoint.GetComponent<CinemachineDollyCart>().m_Position;

        // 移動先の情報を設定
        foreach (PathData changePathData in _branchPT.PathList)
        {
            ChangeNextPathData data = new ChangeNextPathData();
            data.changePath = changePathData.Path.GetComponent<CinemachinePathBase>();
            data.changePosMin = 0.0f;
            data.changePosMax = changePathData.Path.GetComponent<CinemachinePathBase>().PathLength;
            data.moveDir = changePathData.MoveDir;

            retData.changeNextDataList.Add(data);
        }

        return retData;
    }

    // 戻ってくるときの移動情報を設定
    private PlayerMoveData ComeBackPlayerData(StageBranchPoint _branchPT, PathData pathData)
    {
        PlayerMoveData retData = new PlayerMoveData();
        // 移動元の情報を設定
        retData.NowPath   = pathData.Path.GetComponent<CinemachinePathBase>();
        retData.nowPosMin = 0.0f;
        retData.nowPosMax = pathData.Path.GetComponent<CinemachinePathBase>().PathLength;

        // 移動先の情報を設定
        ChangeNextPathData data = new ChangeNextPathData();
        data.changePath   = _branchPT.BeforeBranchPath;
        data.changePosMin = _branchPT.gameObject.GetComponent<CinemachineDollyCart>().m_Position;
        data.changePosMax = _branchPT.EndPoint.GetComponent<CinemachineDollyCart>().m_Position;
        data.moveDir      = (PlayerMovePath.MoveDir)((int)pathData.MoveDir * -1);

        retData.changeNextDataList.Add(data);


        return retData;
    }
}