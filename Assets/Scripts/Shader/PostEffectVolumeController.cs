using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class PostEffectVolumeController : MonoBehaviour
{
    [SerializeField]
    private Volume volume;

    private Vignette vignette;

    //Vignetteのフェードアウトする時間調整
    [SerializeField,Range(0.0f,1.0f)]
    private float fadeScale;
    // Start is called before the first frame update
    void Start()
    {
        //vignetteを取得
        volume.profile.TryGet<Vignette>(out vignette);
    }

    // Update is called once per frame
    void Update()
    {
        vignette.intensity.value -= Time.deltaTime * fadeScale;
    }

    //vignetteの値変更
    public void SetIntensity(float intens)
    {
        vignette.intensity.value = intens;
    }
}
