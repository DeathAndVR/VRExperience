// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/KeyOut" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	_SecondaryTex("Secondary Texture", 2D) = "white" {}
	_ThresholdRed("Threshold RED", Range(0, 1)) = 0
	_ThresholdBlue("Threshold BLUE", Range(0, 1)) = 0
	}
		SubShader{
		Pass{
		Tags{ "RenderType" = "Transpaernt" }

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
#include "UnityCG.cginc"

		struct v2f {
		float4 pos : SV_POSITION;
		float2 uv1 : TEXCOORD0;
		float2 uv2 : TEXCOORD1;
	};

	sampler2D _MainTex;
	float4 _MainTex_ST;
	sampler2D _SecondaryTex;
	float4 _SecondaryTex_ST;

	v2f vert(appdata_base v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv1 = TRANSFORM_TEX(v.texcoord, _MainTex);
		o.uv2 = TRANSFORM_TEX(v.texcoord, _SecondaryTex);
		return o;
	}

	float _ThresholdRed;
	float _ThresholdBlue;

	fixed4 frag(v2f i) : COLOR{
		fixed4 col1 = tex2D(_MainTex, i.uv1);
	fixed4 col2 = tex2D(_SecondaryTex, i.uv2);
	float val = ceil(saturate(col1.g - col1.r - _ThresholdRed)) * ceil(saturate(col1.g - col1.b - _ThresholdBlue));
	if (val == 1)
	{
		clip(-1.0);
	}
	return lerp(col1, col2, val);
	}

		ENDCG
	}
	}
		FallBack "Diffuse"
}