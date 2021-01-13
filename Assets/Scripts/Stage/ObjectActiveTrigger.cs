using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


[DisallowMultipleComponent]
[RequireComponent(typeof(CinemachineDollyCart))]
// 範囲内に指定のタグのオブジェクトが入ったらオブジェクトをアクティブにするトリガー
public class ObjectActiveTrigger : MonoBehaviour
{
    // コライダーのタグ名
    enum ColliderTagName
    {
        None,
        MagicCircle,
        Player
    }


    // アクティブにさせたいオブジェクト
    [SerializeField,Header("アクティブにさせたいオブジェクト")]
    private List<GameObject> activeObjectList = null;
    // タグの指定
    [SerializeField,Header("下記のタグのオブジェクトを通過したら")]
    private ColliderTagName colliderTag = ColliderTagName.None;

    private void Start()
    {
        // 開始時に非アクティブにする
        foreach(GameObject obj in activeObjectList)
            obj.SetActive(false);

        // 設置補助用のDollyCartを削除
        this.GetComponent<CinemachineDollyCart>().enabled = false;
    }

    // 通過するとアクティブにする
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == this.colliderTag.ToString())
        {
            foreach (GameObject obj in activeObjectList)
                obj.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
