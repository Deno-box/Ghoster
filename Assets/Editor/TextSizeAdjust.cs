using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// テキストボックスをテキストのサイズに合わせる
// Editモードで実行
[ExecuteInEditMode]
public class TextSizeAdjust : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Adjust();
    }

    // サイズを合わせる
    public void Adjust()
    {
        // テキスト取得
        Text text = GetComponent<Text>();
        
        // サイズ変更
        text.rectTransform.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
    }
}
