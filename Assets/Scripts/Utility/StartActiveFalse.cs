using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartActiveFalse : MonoBehaviour
{
    // アタッチされているオブジェクトを非表示にする
    void Start()
    {
        this.gameObject.SetActive(false);   
    }
}
