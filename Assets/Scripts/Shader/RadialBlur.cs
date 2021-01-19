using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using System;

[Serializable, VolumeComponentMenu("Post-processing/Custom/RadialBlur")]
public sealed class RadialBlur : CustomPostProcessVolumeComponent, IPostProcessComponent
{
    [Tooltip("Controls the intensity of the effect.")]
    public ClampedFloatParameter blurIntensity = new ClampedFloatParameter(0f, 0f, 1f);
    [Tooltip("Controls the sampleCount of the effect.")]
    public ClampedFloatParameter sampleCount = new ClampedFloatParameter(1.0f, 1.0f, 8.0f);

    Material m_Material;

    public bool IsActive() => m_Material != null && blurIntensity.value > 0f;

    // Do not forget to add this post process in the Custom Post Process Orders list (Project Settings > HDRP Default Settings).
    public override CustomPostProcessInjectionPoint injectionPoint => CustomPostProcessInjectionPoint.AfterPostProcess;

    const string kShaderName = "Hidden/Shader/RadialBlur";

    public override void Setup()
    {
        if (Shader.Find(kShaderName) != null)
            m_Material = new Material(Shader.Find(kShaderName));
        else
            Debug.LogError($"Unable to find shader '{kShaderName}'. Post Process Volume RadialBlur is unable to load.");
    }

    public override void Render(CommandBuffer cmd, HDCamera camera, RTHandle source, RTHandle destination)
    {
        if (m_Material == null)
            return;
        //ブラーの強さ
        m_Material.SetFloat("_Intensity", blurIntensity.value);
        //サンプルカウント
        m_Material.SetFloat("_Samplecount", sampleCount.value);
        //テクスチャ
        m_Material.SetTexture("_InputTexture", source);

        //HDUtils.DrawFullScreen(cmd, m_Material, destination);
        Graphics.Blit(source, destination, m_Material);
    }

    public override void Cleanup()
    {
        CoreUtils.Destroy(m_Material);
    }
}
