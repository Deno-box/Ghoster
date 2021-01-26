using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public enum FadeType
    {
        FadeOut = -1,
        None,
        FadeIn
    }
    // 自身のイメージ
    [SerializeField]
    private List<Image> imageList = new List<Image>();
    // フェード中かどうか
    private bool isFade = false;
    // 透明度
    private float alpha = 0.0f;

    // フェードのタイプと速度を指定
    // フェードが完了したらtrueを返す
    public bool Execute(FadeType _type,float _fadeSpeed)
    {
        bool ret = false;

        foreach (Image image in imageList)
        {
            Color color = image .color;

            float alpha = color.a + (_fadeSpeed * Time.deltaTime * (float)_type);
            image.color = new Color(color.r, color.g, color.b, alpha);

            // フェードが完了したらtrueを返す
            if (alpha > 1.0f || alpha < 0.0f)
                ret = true;
            else
                ret = false;
        }

        // 全てのオブジェクトのフェードが完了していたらtrueを返す
        return ret;
    }
}
