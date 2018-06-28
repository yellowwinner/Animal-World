// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Seamless2" {
	Properties{
		_MainTex("Texture", 2D) = ""
		_Scale("Texture Scale Multiplier", Float) = 0.1
	}

	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			float _Scale;

			//vertex data inputs are declared in a structure
			struct vOut {
				fixed4 texCoords : TEXCOORD0; 
				fixed4 position : POSITION;
			};

			//appdata_base: position, normal and one texture coordinate
			vOut vert(appdata_base i) {
				vOut o;

				//transforms a point from object space to the camera’s clip space in homogeneous coordinates.
				o.position = UnityObjectToClipPos(i.vertex);

				o.texCoords = i.texcoord;

				float3 worldPos = mul(unity_ObjectToWorld, i.vertex).xyz;
                     
				float3 worldNormal = normalize(mul(float4(i.normal, 0.0 ), unity_WorldToObject).xyz);
				if (worldNormal.y > 0.5)
				    o.texCoords.xy = worldPos.xz; // top
				else if (abs(worldNormal.x) > 0.5)
				    o.texCoords.xy = worldPos.yz; // side
				else
				o.texCoords.xy = worldPos.xy; // front         

				return o;
			}

			fixed4 frag(vOut f) : COLOR{
				fixed4 mainTex = tex2D(_MainTex, f.texCoords.xy * _Scale);
				return mainTex;
			}

			ENDCG
		}
	}
}
