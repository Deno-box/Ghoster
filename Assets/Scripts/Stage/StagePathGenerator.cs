using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Candlelight;

using Cinemachine;

public class StagePathGenerator : MonoBehaviour
{
    // パスのリスト
    [SerializeField,PropertyBackingField("PathList")]
    private List<GameObject> pathList = new List<GameObject>();
    // 値が変更されたら
    public List<GameObject> PathList
    {
        get { return pathList; }
        set 
        { 
            // 値を設定
            pathList = value;

            // リストを更新
            UpdatePathList();

            // パスのリスト数を更新
            listLastSize = pathList.Count;
        }
    }

    // 分岐点のプレハブ
    [SerializeField]
    private GameObject branchPointPrefab = null;
    // 一つ前の段階のパスリストの数
    private int listLastSize = 0;


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

    }
}
