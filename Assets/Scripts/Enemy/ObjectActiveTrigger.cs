using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;




// 範囲内にPlayerが入ったらEnemyの移動を開始させるトリガー
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
    [SerializeField]
    private List<GameObject> activeObjectList = null;
    [SerializeField]
    private ColliderTagName colliderTag = ColliderTagName.None;

    private void Start()
    {
        // 開始時に非アクティブにする
        foreach(GameObject obj in activeObjectList)
            obj.SetActive(false);
    }

    private void Update()
    {
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
