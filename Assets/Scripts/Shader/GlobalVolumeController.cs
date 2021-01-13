using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class GlobalVolumeController : MonoBehaviour
{
    [SerializeField]
    Volume volume;

    [SerializeField]
    float timeScale = 0.0f;

    Vignette vignette;

    private void Update()
    {
        // vignetteの強度を変更
        volume.profile.TryGet(out vignette);
        vignette.intensity.value -= Time.deltaTime * timeScale;
    }

    // ダメージを受けた際に変更する
    public void ChangeVignette(float vig)
    {
        vignette.intensity.value = vig;
    }
    
    // ブルーム効果を消す
    public void RemoveBloom()
    {
        volume.profile.Remove<Bloom>();
    }

    // 画面周りの効果を消す
    public void RemoveVignette()
    {
        volume.profile.Remove<Vignette>();
    }

    // 被写体深度効果を消す
    public void RemoveDoF()
    {
        volume.profile.Remove<DepthOfField>();
    }
}
