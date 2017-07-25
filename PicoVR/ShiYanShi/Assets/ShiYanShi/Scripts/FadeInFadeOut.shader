﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/FadeInFadeOut"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "black" {}
		_Float1("Float1",Float)=0.0
	}
	SubShader
	{
		// No culling or depth
		//Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			//#include "UnityCG.cginc"
			
			uniform sampler2D _MainTex;
			uniform float _Float1;

			struct Input
			{
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 pos : SV_POSITION;
			};

			v2f vert (Input i)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(i.pos);
				o.uv = i.uv;
				return o;
			}
			
			

			float4 frag (v2f i) : COLOR
			{
				float4 outColor;
				outColor = tex2D(_MainTex, i.uv) + _Float1;
				return outColor;
			}
			ENDCG
		}
	}
	Fallback off
}
