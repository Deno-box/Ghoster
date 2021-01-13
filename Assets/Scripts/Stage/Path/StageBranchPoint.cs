using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Candlelight;
using Cinemachine;

public class StageBranchPoint : MonoBehaviour
{
    // 分岐前のパスを設定
    [SerializeField, Header("分岐前のパス"), PropertyBackingField("BeforeBranchPath")]
    private CinemachinePathBase beforeBranchPath;

    // パスのリスト
    [SerializeField, Header("分岐後のパス")]
    public List<PathData> pathList = new List<PathData>();
    public IReadOnlyList<PathData> PathList{ get { return pathList; } }

    // 分岐点のプレハブ
    //[SerializeField]
    private GameObject branchPointPrefab = null;
    // 一つ前の段階のパスリストの数
    private int listLastSize = 0;

    //// 分岐点の色を設定
    //[SerializeField, PropertyBackingField("BranchColor")]
    //private Color branchColor;

    [SerializeField, Header("座標"), PropertyBackingField("PathPos")]
    private float position;

    [SerializeField]
    GameObject endPoint = null;
    public GameObject EndPoint
    {
        get { return endPoint; }
    }

    // 分岐前のパスを設定
    public CinemachinePathBase BeforeBranchPath
    {
        get { return beforeBranchPath; }
        set
        {
            beforeBranchPath = value;
            this.GetComponent<CinemachineDollyCart>().m_Path = beforeBranchPath;
        }
    }

    // パスの位置を設定
    public float PathPos
    {
        get { return position; }
        set
        {
            position = value;

            position = Mathf.Clamp(position, 0.0f, this.GetComponent<CinemachineDollyCart>().m_Path.PathLength);

            this.GetComponent<CinemachineDollyCart>().m_Position = position;
            endPoint.GetComponent<CinemachineDollyCart>().m_Path = this.GetComponent<CinemachineDollyCart>().m_Path;
            endPoint.GetComponent<CinemachineDollyCart>().m_Position = pathList[0].Path.GetComponent<CinemachinePathBase>().PathLength + position;
        }
    }

    // 分岐点の色を設定
    //public Color BranchColor
    //{
    //    get { return branchColor; }
    //    set
    //    {
    //        branchColor = value;
    //        Renderer rend = this.GetComponent<Renderer>();
    //        rend.sharedMaterial.shader = Shader.Find("HDRP/Lit");
    //        rend.sharedMaterial.SetColor("_BaseColor", branchColor);

    //        Renderer endPointRend = endPoint.GetComponent<Renderer>();
    //        endPointRend.sharedMaterial.shader = Shader.Find("HDRP/Lit");
    //        endPointRend.sharedMaterial.SetColor("_BaseColor", branchColor);
    //    }
    //}

    private void Update()
    {
        // リストを更新
        //UpdatePathList();

        // パスのリスト数を更新
        //listLastSize = pathList.Count;
    }


    void UpdatePathList()
    {
        // リストの更新
        for (int i = 0; i < pathList.Count; i++)
        {
            // 新しいリストが追加されていたら
            if (i >= listLastSize)
            {
                // パスを追加
                AddPath();
            }
        }
    }

    // パスを削除
    void DestroyPath()
    {

    }

    // パスを追加
    void AddPath()
    {
        //GameObject obj = Instantiate(branchPointPrefab, this.transform);
    }
}

[System.Serializable]
public class PathData
{
    [Header("移動先の情報")]
    [SerializeField]
    private GameObject path  = null;
    public GameObject Path { get { return path; } }

    [SerializeField]
    private PlayerMovePath.MoveDir moveDir = PlayerMovePath.MoveDir.Right;
    public PlayerMovePath.MoveDir MoveDir { get { return moveDir; } }
}