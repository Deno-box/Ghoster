Shader "Hidden/Shader/RadialBlur"
{    
	HLSLINCLUDE

	//uniform sampler2D _MainTex;
	//uniform float _BlurDegree;	//模糊强度（0-0.05）
	//uniform float4 _BlurCenter; //模糊中心点xy值（0-1）屏幕空间
	//#include "UnityCG.cginc"
	#define SAMPLE_COUNT 6		//迭代次数



    #pragma target 4.5
    #pragma only_renderers d3d11 ps4 xboxone vulkan metal switch

    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
    #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/FXAA.hlsl"
    #include "Packages/com.unity.render-pipelines.high-definition/Runtime/PostProcessing/Shaders/RTUpscale.hlsl"

    struct Attributes
    {
        uint vertexID : SV_VertexID;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };

    struct Varyings
    {
        float4 positionCS : SV_POSITION;
        float2 texcoord   : TEXCOORD0;
        UNITY_VERTEX_OUTPUT_STEREO
    };

    Varyings Vert(Attributes input)
    {
        Varyings output;
        UNITY_SETUP_INSTANCE_ID(input);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
        output.positionCS = GetFullScreenTriangleVertexPosition(input.vertexID);
        output.texcoord = GetFullScreenTriangleTexCoord(input.vertexID);
        return output;
    }

    // List of properties to control your post process effect
    float _Intensity;
    TEXTURE2D_X(_InputTexture);
	half _SampleCount;

	//ここにシェーダーを書く
    float4 CustomPostProcess(Varyings input) : SV_Target
    {
		//模糊方向为模糊中点指向边缘（当前像素点），而越边缘该值越大，越模糊
		float2 dir = i.uv - _BlurCenter.xy;
		float4 outColor = 0;
		//采样SAMPLE_COUNT次
		for (int j = 0; j < SAMPLE_COUNT; ++j)
		{
			//计算采样uv值：正常uv值+从中间向边缘逐渐增加的采样距离
			float2 uv = i.uv + _BlurDegree * dir * j;
			outColor += tex2D(_MainTex, uv);
		}
		//取平均值
		outColor /= SAMPLE_COUNT;
		return outColor;
	}

    ENDHLSL

    SubShader
    {
        Pass
        {
            Name "RadialBlur"

            ZWrite Off
            ZTest Always
            Blend Off
            Cull Off

            HLSLPROGRAM
                #pragma fragment CustomPostProcess
                #pragma vertex Vert
            ENDHLSL
        }
    }
    Fallback Off
}
