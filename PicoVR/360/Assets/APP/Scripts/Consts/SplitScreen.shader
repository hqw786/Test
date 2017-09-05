// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Camera/SplitScreen" 
{
	Properties 
	{
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM

			#pragma vertex vert 
			#pragma fragment frag 
			#include "UnityCG.cginc" 

			#pragma target 3.0


			struct appdata_t 
			{ 
				float4 vertex : POSITION; 
				float2 texcoord : TEXCOORD0; 
			}; 

			struct v2f 
			{ 
				float4 vertex : SV_POSITION; 
				half2 texcoord : TEXCOORD0; 
			}; 

			uniform sampler2D _MainTex; 
			uniform float4 _MainTex_ST; 


			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord,_MainTex);
				return o;
			}

			fixed4 frag(v2f IN) : Color 
			{ 
				
				fixed2 calculate = IN.texcoord;
				half4 color;
				if(IN.texcoord.x < 0.5)
				{
					calculate.x = calculate.x * 2;
				}
				else
				{
					calculate.x = calculate.x * 2 - 1;
				}
				color = tex2D(_MainTex,  calculate); 
				//half4 color = tex2D(_MainTex, IN.texcoord);
				fixed4 finalColor = color;
				return finalColor;
			} 
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
